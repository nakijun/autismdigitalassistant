;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;                                                                     ;;;
;;;                  Language Technologies Institute                    ;;;
;;;                     Carnegie Mellon University                      ;;;
;;;                         Copyright (c) 2000                          ;;;
;;;                        All Rights Reserved.                         ;;;
;;;                                                                     ;;;
;;; Permission is hereby granted, free of charge, to use and distribute ;;;
;;; this software and its documentation without restriction, including  ;;;
;;; without limitation the rights to use, copy, modify, merge, publish, ;;;
;;; distribute, sublicense, and/or sell copies of this work, and to     ;;;
;;; permit persons to whom this work is furnished to do so, subject to  ;;;
;;; the following conditions:                                           ;;;
;;;  1. The code must retain the above copyright notice, this list of   ;;;
;;;     conditions and the following disclaimer.                        ;;;
;;;  2. Any modifications must be clearly marked as such.               ;;;
;;;  3. Original authors' names are not deleted.                        ;;;
;;;  4. The authors' names are not used to endorse or promote products  ;;;
;;;     derived from this software without specific prior written       ;;;
;;;     permission.                                                     ;;;
;;;                                                                     ;;;
;;; CARNEGIE MELLON UNIVERSITY AND THE CONTRIBUTORS TO THIS WORK        ;;;
;;; DISCLAIM ALL WARRANTIES WITH REGARD TO THIS SOFTWARE, INCLUDING     ;;;
;;; ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS, IN NO EVENT  ;;;
;;; SHALL CARNEGIE MELLON UNIVERSITY NOR THE CONTRIBUTORS BE LIABLE     ;;;
;;; FOR ANY SPECIAL, INDIRECT OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES   ;;;
;;; WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN  ;;;
;;; AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION,         ;;;
;;; ARISING OUT OF OR IN CONNECTION WITH THE USE OR PERFORMANCE OF      ;;;
;;; THIS SOFTWARE.                                                      ;;;
;;;                                                                     ;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;             Author: Alan W Black (awb@cs.cmu.edu)                   ;;;
;;;               Date: January 2000                                    ;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;                                                                     ;;;
;;; Generate a C compilable CART trees.                                 ;;;
;;;                                                                     ;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;


(define (carttoC name tree odir)
  "(carttoC NAME TREE ODIR)
Coverts a CART tree to a single C file called ODIR/NAME_cart.c."
  (let 
    ((ofdc (fopen (path-append odir (string-append name "_cart.c")) "w"))
     (ofdh(fopen (path-append odir (string-append name "_cart.h")) "w")))
    (format ofdc "/*******************************************************/\n")
    (format ofdc "/**  Autogenerated cart trees for %s    */\n" name)
    (format ofdc "/*******************************************************/\n")
    (format ofdc "\n")
    (format ofdc "#include \"cst_string.h\"\n")
    (format ofdc "#include \"cst_cart.h\"\n")
    (format ofdc "#include \"cst_regex.h\"\n")
    (format ofdc "#include \"%s_cart.h\"\n" name)

    (format ofdh "/*******************************************************/\n")
    (format ofdh "/**  Autogenerated cart tree for %s    */\n" name)
    (format ofdh "/**  from %s    */\n" odir)
    (format ofdh "/*******************************************************/\n")
    (format ofdh "\n")

    (set! current_node -1)
    (set! val_table nil)
    (set! feat_nums nil)

    (do_carttoC ofdc ofdh name tree)

    (fclose ofdc)
    (fclose ofdh)
    ))

(define (do_carttoC ofdc ofdh name tree)
  "(do_carttoC ofdc ofdh name tree)
Do the tree dump, this section is split off for cases when we 
want multiple trees in the same file."

    (set! cart_name name)
    (format ofdc "\n\n")
    (format ofdc "static const cst_cart_node %s_cart_nodes[] = {\n" name)
    (carttoC_tree_nodes tree ofdc ofdh)
    (format ofdc "{ 255, CST_CART_OP_NONE, 0, 0}};\n\n")

    ;; definitions are in the .h file    
;    (mapcar
;     (lambda (f)
;       (format ofdc "%s" (caddr f)))
;     (reverse val_table))
    (format ofdc "\n\n")

    (format ofdc "static const char * const %s_feat_table[] = {\n" name)
    (mapcar 
     (lambda (f)
       (if (string-equal "string" (typeof (car f)))
	   (format ofdc "%s,\n" (car f))
	   (format ofdc "\"%s\",\n" (car f))))
     (reverse feat_nums))
    (format ofdc "NULL };\n\n")

    (format ofdc "const cst_cart %s_cart = {\n" name)
    (format ofdc "  %s_cart_nodes,\n" name)
    (format ofdc "  %s_feat_table\n" name)
    (format ofdc "};\n")

)

(defvar cart_operators
  '(("is" "CST_CART_OP_IS")
    ("in" "CST_CART_OP_IN")
    ("<" "CST_CART_OP_LESS")
    (">" "CST_CART_OP_GREATER")
    ("matches" "CST_CART_OP_MATCHES")
    ("=" "CST_CART_OP_EQUALS")))

(define (carttoC_feat_num f)
  (let ((fn (assoc_string f feat_nums)))
    (cond
     (fn
      (cadr fn))
     (t
      (set! feat_nums 
	    (cons (list f (length feat_nums))
		  feat_nums))
      (carttoC_feat_num f)))))

(define (carttoC_val_table ofdh f operator)
  (let ((fn (assoc_string
	     (if (string-equal operator "is")
		 (format nil "is_%s" f)
		 f)
	     val_table)))
    (cond
     (fn
      (cadr fn))
     (t
      (let ((nname (format nil "val_%04d" (length val_table))))
;	(format ofdh "static const cst_val %s;\n" nname)
	(set! val_table
	      (cons (list 
		     (if (string-equal operator "is")
			 (format nil "is_%s" f)
			 f)
		     nname
		     (cond
		      ((string-equal operator "is")
		       (format ofdh "DEF_STATIC_CONST_VAL_STRING(%s,\"%s\");\n"
			       nname f))
		      ((string-equal "matches" operator)
		       (format ofdh "DEF_STATIC_CONST_VAL_INT(%s,CST_RX_%s_NUM);\n"
			       nname f))
		      ((number? f)
		       (format ofdh "DEF_STATIC_CONST_VAL_FLOAT(%s,%f);\n"
			       nname f))
		      ((consp f)
		       (format stderr "list vals not supported here yet\n")
		       (error f))
		      (t
		       (format ofdh "DEF_STATIC_CONST_VAL_STRING(%s,\"%s\");\n"
			       nname f))))
		    val_table))
	(carttoC_val_table ofdh f operator))))))

(define (carttoC_tree_nodes tree ofdc ofdh)
  "(carttoC_tree_nodes tree ofdc ofdh)
Dump the nodes in the tree."
  (let ((this_node (set! current_node (+ 1 current_node))))
    (cond
     ((cdr tree) ;; a question node
      (format ofdc "{ %d, %s, %s, (cst_val *)&%s},\n"
	      (carttoC_feat_num (caar tree))  ;; the feature
	      (cadr (assoc_string             ;; operator
		     (cadr (car tree))
		     cart_operators))
	      (format nil "CTNODE_%s_NO_%04d" cart_name this_node); the no node
	      (carttoC_val_table ofdh 
				 (nth 2 (car tree)) 
				 (cadr (car tree))))
      (carttoC_tree_nodes (car (cdr tree)) ofdc ofdh)
      (format ofdh "#define CTNODE_%s_NO_%04d %d\n" cart_name
	      this_node (+ 1 current_node))
      (carttoC_tree_nodes (car (cdr (cdr tree))) ofdc ofdh))
     (t  ;; a leaf node
      (format ofdc
	      "{ 255, CST_CART_OP_NONE, 0, (cst_val *)&%s },\n"
	      (carttoC_extract_answer ofdh tree))))))

(define (carttoC_extract_answer ofdh tree)
  "(carttoC_extract_answer tree)
Get answer from leaf node.  (this can be redefined if you want different
behaviour)."
  (carttoC_val_table ofdh 
		     (car (last (car tree)))
		     'none))

(provide 'make_cart)
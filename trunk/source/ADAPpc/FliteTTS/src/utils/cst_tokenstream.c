/*************************************************************************/
/*                                                                       */
/*                  Language Technologies Institute                      */
/*                     Carnegie Mellon University                        */
/*                        Copyright (c) 1999                             */
/*                        All Rights Reserved.                           */
/*                                                                       */
/*  Permission is hereby granted, free of charge, to use and distribute  */
/*  this software and its documentation without restriction, including   */
/*  without limitation the rights to use, copy, modify, merge, publish,  */
/*  distribute, sublicense, and/or sell copies of this work, and to      */
/*  permit persons to whom this work is furnished to do so, subject to   */
/*  the following conditions:                                            */
/*   1. The code must retain the above copyright notice, this list of    */
/*      conditions and the following disclaimer.                         */
/*   2. Any modifications must be clearly marked as such.                */
/*   3. Original authors' names are not deleted.                         */
/*   4. The authors' names are not used to endorse or promote products   */
/*      derived from this software without specific prior written        */
/*      permission.                                                      */
/*                                                                       */
/*  CARNEGIE MELLON UNIVERSITY AND THE CONTRIBUTORS TO THIS WORK         */
/*  DISCLAIM ALL WARRANTIES WITH REGARD TO THIS SOFTWARE, INCLUDING      */
/*  ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS, IN NO EVENT   */
/*  SHALL CARNEGIE MELLON UNIVERSITY NOR THE CONTRIBUTORS BE LIABLE      */
/*  FOR ANY SPECIAL, INDIRECT OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES    */
/*  WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN   */
/*  AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION,          */
/*  ARISING OUT OF OR IN CONNECTION WITH THE USE OR PERFORMANCE OF       */
/*  THIS SOFTWARE.                                                       */
/*                                                                       */
/*************************************************************************/
/*             Author:  Alan W Black (awb@cs.cmu.edu)                    */
/*               Date:  July 1999                                        */
/*************************************************************************/
/*                                                                       */
/*  Tokenizer for strings and files                                      */
/*                                                                       */
/*************************************************************************/
#include "cst_tokenstream.h"

const unsigned char * const cst_ts_default_whitespacesymbols = " \t\n\r";
const unsigned char * const cst_ts_default_singlecharsymbols = "(){}[]";
const unsigned char * const cst_ts_default_prepunctuationsymbols = "\"'`({[";
const unsigned char * const cst_ts_default_postpunctuationsymbols = "\"'`.,:;!?(){}[]";

#define TS_BUFFER_SIZE 256
#define TS_EOF -1

static unsigned char ts_getc(cst_tokenstream *ts);

static void set_charclass_table(cst_tokenstream *ts)
{
    int i;
    memset(ts->charclass,0,256);  /* zero everything */
    
    for (i=0; ts->p_whitespacesymbols[i]; i++)
	ts->charclass[ts->p_whitespacesymbols[i]] |= TS_CHARCLASS_WHITESPACE;
    for (i=0; ts->p_singlecharsymbols[i]; i++)
	ts->charclass[ts->p_singlecharsymbols[i]] |= TS_CHARCLASS_SINGLECHAR;
    for (i=0; ts->p_prepunctuationsymbols[i]; i++)
	ts->charclass[ts->p_prepunctuationsymbols[i]] |= TS_CHARCLASS_PREPUNCT;
    for (i=0; ts->p_postpunctuationsymbols[i]; i++)
	ts->charclass[ts->p_postpunctuationsymbols[i]]|=TS_CHARCLASS_POSTPUNCT;
    return;
}

void set_charclasses(cst_tokenstream *ts,
		     const unsigned char *whitespace,
		     const unsigned char *singlecharsymbols,
		     const unsigned char *prepunctuation,
		     const unsigned char *postpunctuation)
{
    ts->p_whitespacesymbols = 
	(whitespace ? whitespace : cst_ts_default_whitespacesymbols);
    ts->p_singlecharsymbols = 
    (singlecharsymbols ? singlecharsymbols : cst_ts_default_singlecharsymbols);
    ts->p_prepunctuationsymbols = 
    (prepunctuation ? prepunctuation : cst_ts_default_prepunctuationsymbols);
    ts->p_postpunctuationsymbols =
   (postpunctuation ? postpunctuation : cst_ts_default_postpunctuationsymbols);

    set_charclass_table(ts);
    return;
}

static void extend_buffer(unsigned char **buffer,int *buffer_max)
{
    int new_max;
    unsigned char *new_buffer;

    new_max = (*buffer_max)+(*buffer_max)/5;
    new_buffer = cst_alloc(unsigned char,new_max);
    memmove(new_buffer,*buffer,*buffer_max);
    cst_free(*buffer);
    *buffer = new_buffer;
    *buffer_max = new_max;
}			  

static cst_tokenstream *new_tokenstream(const unsigned char *whitespace,
					const unsigned char *singlechars,
					const unsigned char *prepunct,
					const unsigned char *postpunct)
{   /* Constructor function */
    cst_tokenstream *ts = cst_alloc(cst_tokenstream,1);
    ts->fd = NULL;
    ts->file_pos = 0;
    ts->line_number = 0;
    ts->string_buffer = NULL;
    ts->token_pos = 0;
    ts->whitespace = cst_alloc(unsigned char,TS_BUFFER_SIZE);
    ts->ws_max = TS_BUFFER_SIZE;
    ts->prepunctuation = cst_alloc(unsigned char,TS_BUFFER_SIZE);
    ts->prep_max = TS_BUFFER_SIZE;
    ts->token = cst_alloc(unsigned char,TS_BUFFER_SIZE);
    ts->token_max = TS_BUFFER_SIZE;
    ts->postpunctuation = cst_alloc(unsigned char,TS_BUFFER_SIZE);
    ts->postp_max = TS_BUFFER_SIZE;

    set_charclasses(ts,whitespace,singlechars,prepunct,postpunct);
    ts->current_char = 0;

    return ts;
}

void delete_tokenstream(cst_tokenstream *ts)
{
    cst_free(ts->whitespace);
    cst_free(ts->prepunctuation);
    cst_free(ts->token);
    cst_free(ts->postpunctuation);
    cst_free(ts);
}

cst_tokenstream *ts_open(const char *filename,
			 const unsigned char *whitespace,
			 const unsigned char *singlechars,
			 const unsigned char *prepunct,
			 const unsigned char *postpunct)
{
    cst_tokenstream *ts = new_tokenstream(whitespace,
					  singlechars,
					  prepunct,
					  postpunct);

#ifndef UNDER_CE
    if (cst_streq("-",filename))
	ts->fd = stdin;
    else
#endif
	ts->fd = cst_fopen(filename,CST_OPEN_READ|CST_OPEN_BINARY);
    ts_getc(ts);

    if (ts->fd == NULL)
    {
	delete_tokenstream(ts);
	return NULL;
    }
    else
	return ts;
}

cst_tokenstream *ts_open_string(const unsigned char *string,
				const unsigned char *whitespace,
				const unsigned char *singlechars,
				const unsigned char *prepunct,
				const unsigned char *postpunct)
{
    cst_tokenstream *ts = new_tokenstream(whitespace,
					  singlechars,
					  prepunct,
					  postpunct);

    ts->string_buffer = cst_strdup(string);
    ts_getc(ts);

    return ts;
}

void ts_close(cst_tokenstream *ts)
{
    if (ts->fd != NULL)
    {
#ifndef UNDER_CE
	if (ts->fd != stdin)
#endif
	    cst_fclose(ts->fd);
	ts->fd = NULL; /* just in case close gets called twice */
    }
    if (ts->string_buffer != NULL)
    {
	cst_free(ts->string_buffer);
	ts->string_buffer = NULL;
    }
    delete_tokenstream(ts);
}

static void get_token_sub_part(cst_tokenstream *ts,
			       int charclass,
			       unsigned char **buffer,
			       int *buffer_max)
{
    int p;

    for (p=0; ((ts->current_char != TS_EOF) &&
               (ts_charclass(ts->current_char,charclass,ts)) &&
	       (!ts_charclass(ts->current_char,
			      TS_CHARCLASS_SINGLECHAR,ts))); p++)
    {
	if (p >= *buffer_max) extend_buffer(buffer,buffer_max);
	(*buffer)[p] = ts->current_char;
	ts_getc(ts);
    }
    (*buffer)[p] = '\0';
}

/* Can't afford dynamically generate this char class so have separater func */
static void get_token_sub_part_2(cst_tokenstream *ts,
				 int endclass1,
				 unsigned char **buffer,
				 int *buffer_max)
{
    int p;

    for (p=0; ((ts->current_char != TS_EOF) &&
               (!ts_charclass(ts->current_char,endclass1,ts)) &&
	       (!ts_charclass(ts->current_char,
			      TS_CHARCLASS_SINGLECHAR,ts)));
         p++)
    {
	if (p >= *buffer_max) extend_buffer(buffer,buffer_max);
	(*buffer)[p] = ts->current_char;
	ts_getc(ts);
    }
    (*buffer)[p] = '\0';
}

static void get_token_postpunctuation(cst_tokenstream *ts)
{
    int p,t;

    t = cst_strlen(ts->token);
    for (p=t;
	 (p > 0) && 
	     ((ts->token[p] == '\0') ||
	      (ts_charclass(ts->token[p],TS_CHARCLASS_POSTPUNCT,ts)));
	 p--);

    if (t != p)
    {
	if (t-p >= ts->postp_max) 
	    extend_buffer(&ts->postpunctuation,&ts->postp_max);
	/* Copy postpunctuation from token */
	memmove(ts->postpunctuation,&ts->token[p+1],(t-p));
	/* truncate token at postpunctuation */
	ts->token[p+1] = '\0';
    }
}

int ts_eof(cst_tokenstream *ts)
{
    if (ts->current_char == TS_EOF)
	return TRUE;
    else
	return FALSE;
}

static unsigned char ts_getc(cst_tokenstream *ts)
{
    if (ts->fd)
    {
	ts->current_char = cst_fgetc(ts->fd);
    }
    else if (ts->string_buffer)
    {
	if (ts->string_buffer[ts->file_pos] == '\0')
	    ts->current_char = TS_EOF;
	else
	    ts->current_char = ts->string_buffer[ts->file_pos];
    }
    
    if (ts->current_char != TS_EOF)
	ts->file_pos++;
    if (ts->current_char == '\n')
	ts->line_number++;
    return ts->current_char;
}

const unsigned char *ts_get_quoted_token(cst_tokenstream *ts,
					 char quote,
					 char escape)
{
    /* for reading the next quoted token that starts with quote and
       ends with quote, quote may appear only if preceded by escape */
    int l;

    /* Hmm can't change quotes within a ts */
    ts->charclass[(unsigned int)quote] |= TS_CHARCLASS_QUOTE;
    ts->charclass[(unsigned int)escape] |= TS_CHARCLASS_QUOTE;

    /* skipping whitespace */
    get_token_sub_part(ts,TS_CHARCLASS_WHITESPACE,
		       &ts->whitespace,
		       &ts->ws_max);
    ts->token_pos = ts->file_pos - 1;

    if (ts->current_char == quote)
    {   /* go until quote */
	ts_getc(ts);
	l=0;
	while (!ts_eof(ts))
	{
	    get_token_sub_part_2(ts,TS_CHARCLASS_QUOTE,
				 &ts->token,&ts->token_max);
	    if (ts->current_char == escape)
	    {
		ts_getc(ts);
		l = cst_strlen(ts->token);
		if (l+1 >= ts->token_max) 
		    extend_buffer(&ts->token,&ts->token_max);
		ts->token[l] = ts->current_char;
		ts->token[l+1] = '\0';
		ts_getc(ts);
	    }
	    else
		break;
	}
	ts_getc(ts);
    }
    else /* its not quotes, like to be careful dont you */
    {    /* treat is as standard token                  */
	/* Get prepunctuation */
	get_token_sub_part(ts,TS_CHARCLASS_PREPUNCT,
			   &ts->prepunctuation,
			   &ts->prep_max);
	/* Get the symbol itself */
	if (!ts_charclass(ts->current_char,TS_CHARCLASS_SINGLECHAR,ts))
	{
	    if (2 >= ts->token_max) extend_buffer(&ts->token,&ts->token_max);
	    ts->token[0] = ts->current_char;
	    ts->token[1] = '\0';
	    ts_getc(ts);
	}
	else
	    get_token_sub_part_2(ts,
				 TS_CHARCLASS_WHITESPACE,    /* end class1 */
				 &ts->token,
				 &ts->token_max);
	/* This'll have token *plus* post punctuation in ts->token */
	/* Get postpunctuation */
	get_token_postpunctuation(ts);
    }

    return ts->token;
}

const unsigned char *ts_get(cst_tokenstream *ts)
{
    /* Get next token */

    /* Skip whitespace */
    get_token_sub_part(ts,
		       TS_CHARCLASS_WHITESPACE,
		       &ts->whitespace,
		       &ts->ws_max);

    /* quoted strings currently ignored */
    ts->token_pos = ts->file_pos - 1;
	
    /* Get prepunctuation */
    if (ts->current_char != TS_EOF &&
        ts_charclass(ts->current_char,TS_CHARCLASS_PREPUNCT,ts))
	get_token_sub_part(ts,
			   TS_CHARCLASS_PREPUNCT,
			   &ts->prepunctuation,
			   &ts->prep_max);
    else
	ts->prepunctuation[0] = '\0';
    /* Get the symbol itself */
    if (ts->current_char != TS_EOF &&
        ts_charclass(ts->current_char,TS_CHARCLASS_SINGLECHAR,ts))
    {
	if (2 >= ts->token_max) extend_buffer(&ts->token,&ts->token_max);
	ts->token[0] = ts->current_char;
	ts->token[1] = '\0';
	ts_getc(ts);
    }
    else
	get_token_sub_part_2(ts,
			     TS_CHARCLASS_WHITESPACE,       /* end class1 */
			     &ts->token,
			     &ts->token_max);
    /* This'll have token *plus* post punctuation in ts->token */
    /* Get postpunctuation */
    get_token_postpunctuation(ts);

    return ts->token;
}

int ts_read(void *buff, int size, int num, cst_tokenstream *ts)
{
    /* people should complain about the speed here */
    /* people will complain about EOF as end of file */
    int i,j,p;
    unsigned char *cbuff;

    cbuff = (unsigned char *)buff;

    for (p=i=0; i < num; i++)
	for (j=0; j < size; j++,p++)
	    cbuff[p] = ts_getc(ts);

    return i;
}

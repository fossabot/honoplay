﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Honoplay.Application.Infrastructure
{
    public sealed class ResponseModel<T>
    {
        /// <summary>
        /// Tek bir kayıt için yapıcı metod
        /// </summary>
        /// <param name="source"></param>
        public ResponseModel(T source) : this(numberOfTotalItems: 1, numberOfSkippedItems: 0, source: new[] { source })
        {
        }

        /// <summary>
        /// Hata mesajı listesi için yapıcı metod
        /// </summary>
        /// <param name="errors"></param>
        public ResponseModel(IEnumerable<Error> errors) : this(numberOfTotalItems: 0, numberOfSkippedItems: 0, source: new List<T>())
        {
            Errors = errors;
        }

        /// <summary>
        /// Tek bir hata mesajı için yapıcı metod
        /// </summary>
        /// <param name="error"></param>
        public ResponseModel(Error error) : this(errors: new[] { error })
        {
        }

        /// <summary>
        /// Hata mesajı listesi ile birlikte liste için yapıcı metod
        /// </summary>
        /// <param name="numberOfTotalItems"></param>
        /// <param name="numberOfSkippedItems"></param>
        /// <param name="source"></param>
        /// <param name="errors"></param>
        public ResponseModel(long numberOfTotalItems, long numberOfSkippedItems, IEnumerable<T> source, IEnumerable<Error> errors) : this(numberOfTotalItems: numberOfTotalItems, numberOfSkippedItems: numberOfSkippedItems, source: source)
        {
            Errors = errors;
        }

        /// <summary>
        /// Hata mesajı listesi ile birlikte tek bir kayıt için yapıcı metod
        /// </summary>
        /// <param name="source"></param>
        /// <param name="errors"></param>
        public ResponseModel(T source, IEnumerable<Error> errors) : this(numberOfTotalItems: 1, numberOfSkippedItems: 0, source: new[] { source })
        {
            Errors = errors;
        }

        /// <summary>
        /// Liste için yapıcı metod
        /// </summary>
        /// <param name="numberOfTotalItems"></param>
        /// <param name="numberOfSkippedItems"></param>
        /// <param name="source"></param>
        public ResponseModel(long numberOfTotalItems, long numberOfSkippedItems, IEnumerable<T> source)
        {
            Items = source;
            NumberOfItems = Items.Count();
            NumberOfTotalItems = numberOfTotalItems;
            NumberOfSkippedItems = numberOfSkippedItems;
            DataType = typeof(T).Name;
        }

        public long NumberOfTotalItems { get; private set; }
        public long NumberOfItems { get; private set; }
        public long NumberOfSkippedItems { get; private set; }

        public string DataType { get; private set; }

        public IEnumerable<T> Items { get; private set; }

        public IEnumerable<Error> Errors { get; private set; }
    }

    public sealed class Error
    {
        public Error(string errorCode, string errorType, string errorDetail)
        {
            ErrorCode = errorCode;
            ErrorType = errorType;
            ErrorDetail = errorDetail;
        }

        public Error(int errorCode, string errorType, string errorDetail)
        {
            ErrorCode = errorCode.ToString();
            ErrorType = errorType;
            ErrorDetail = errorDetail;
        }

        public Error(int errorCode, string errorType, Exception ex)
        {
            ErrorCode = errorCode.ToString();
            ErrorType = errorType;
            ErrorDetail = ex.Message;
        }

        public string ErrorCode { get; set; }
        public string ErrorType { get; set; }
        public string ErrorDetail { get; set; }
    }
}
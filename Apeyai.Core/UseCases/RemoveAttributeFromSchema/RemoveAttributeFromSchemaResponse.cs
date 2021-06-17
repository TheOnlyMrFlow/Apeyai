﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apeyai.Core.Common.UseCases;

namespace Apeyai.Core.UseCases.RemoveAttributeFromSchema
{
    public class RemoveAttributeFromSchemaResponse: IUseCaseResponse
    {
        public enum ERemoveAttributeFromSchemaError
        {
            SchemaNotFound,
            AttributeNotFound,
            Unknown
        }

        public ERemoveAttributeFromSchemaError? Error { get; internal set; }

        public bool Success => Error == null;
    }
}

﻿using System;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Utilities.Testing;
using TRepo = Jlw.Data.LocalizedContent.LocalizedContentFieldRepository;

namespace Jlw.Data.LocalizedContent.Tests
{
    public class LocalizedContentFieldRepositoryTestSchema : BaseModelSchema<TRepo>
    {
        void InitConstructors()
        {
            AddConstructor(Public, new Type[] { typeof(IModularDbClient), typeof(string) });
            AddConstructor(Public, new Type[] { typeof(IModularDbOptions) });
        }

        void InitProperties()
        {
        }

        void InitFields()
        {
        }

        void InitInterfaces()
        {
            AddInterface(typeof(ILocalizedContentFieldRepository));
            AddInterface(typeof(IModularDataRepositoryBase<ILocalizedContentField, LocalizedContentField>));
        }

        public LocalizedContentFieldRepositoryTestSchema()
        {
            InitConstructors();
            InitProperties();
            InitFields();
            InitInterfaces();
        }


    }
}

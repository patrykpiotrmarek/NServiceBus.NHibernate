﻿namespace NServiceBus.NHibernate.Tests.API
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using NUnit.Framework;
    using PublicApiGenerator;

    [TestFixture]
    public class APIApprovals
    {
#if NET461
        [Test]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Approve__NET461()
        {
            var publicApi = Filter(ApiGenerator.GeneratePublicApi(typeof(NHibernatePersistence).Assembly));
            TestApprover.Verify(publicApi);
        }
#endif

#if NETCOREAPP2_0
        [Test]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Approve__NETSTANDARD2_0()
        {
            var publicApi = Filter(ApiGenerator.GeneratePublicApi(typeof(Endpoint).Assembly));
            TestApprover.Verify(publicApi);
        }
#endif

        static string Filter(string text)
        {
            return string.Join(Environment.NewLine, text.Split(new[]
                {
                    Environment.NewLine
                }, StringSplitOptions.RemoveEmptyEntries)
                .Where(l => !l.StartsWith("[assembly: ReleaseDateAttribute("))
                .Where(l => !string.IsNullOrWhiteSpace(l))
            );
        }
    }
}
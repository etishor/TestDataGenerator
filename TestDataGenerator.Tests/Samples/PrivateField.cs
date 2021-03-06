﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using System.Runtime.Serialization;


namespace TestDataGenerator.Tests.Samples
{



    public class PrivateField : IAssertEquality
    {


        private int Value;

        public static PrivateField CreateInstance()
        {
            return new PrivateField { Value = 10 };
        }

        public void AssertEquality(object other)
        {
            other.Should().NotBeNull();
            other.Should().BeOfType<PrivateField>();
            PrivateField target = other as PrivateField;

            target.Value.Should().Be(this.Value);
        }
    }
}

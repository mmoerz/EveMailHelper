using System;

namespace EveMailHelper.Test.Tools
{
    public abstract class TheoryData : ITheoryData
    {
        public abstract object[] ToParameterArray();
        public static ITheoryData Factory<TSystemUnderTest, TExpectedOutput>
            (TSystemUnderTest sut, TExpectedOutput expectedOutput, string description)
        {
            var data = new TheoryData<TSystemUnderTest, TExpectedOutput>
            {
                SystemUnderTest = sut,
                Description = description,
                ExpectedOutput = expectedOutput
            };
            return data;
        }
        public static ITheoryData Factory2<TSystemUnderTest, TSystemUnderTest2, TExpectedOutput>
            (TSystemUnderTest sut, TSystemUnderTest2 sut2, TExpectedOutput expectedOutput, string description)
        {
            var data = new TheoryData2<TSystemUnderTest, TSystemUnderTest2, TExpectedOutput>
            {
                SystemUnderTest = sut,
                SystemUnderTest2 = sut2,
                Description = description,
                ExpectedOutput = expectedOutput
            };
            return data;
        }

        public static ITheoryData Factory3<TSystemUnderTest, TSystemUnderTest2, TSystemUnderTest3, TExpectedOutput>
            (TSystemUnderTest sut, TSystemUnderTest2 sut2, TSystemUnderTest3 sut3,
            TExpectedOutput expectedOutput, string description)
        {
            var data = new TheoryData3<TSystemUnderTest, TSystemUnderTest2, TSystemUnderTest3, TExpectedOutput>
            {
                SystemUnderTest = sut,
                SystemUnderTest2 = sut2,
                SystemUnderTest3 = sut3,
                Description = description,
                ExpectedOutput = expectedOutput
            };
            return data;
        }

        public static ITheoryData FailFactory<TSystemUnderTest>
            (TSystemUnderTest sut, string description)
        {
            var data = new TheoryFailData<TSystemUnderTest>
            {
                SystemUnderTest = sut,
                Description = description
            };
            return data;
        }
        public static ITheoryData FailFactory2<TSystemUnderTest, TSystemUnderTest2>
            (TSystemUnderTest sut, TSystemUnderTest2 sut2, string description)
        {
            var data = new TheoryFailData2<TSystemUnderTest, TSystemUnderTest2>
            {
                SystemUnderTest = sut,
                SystemUnderTest2 = sut2,
                Description = description
            };
            return data;
        }
    }

    public class TheoryData<TSystemUnderTest, TExpectedOutput> : TheoryData
    {
        public TSystemUnderTest? SystemUnderTest { get; set; }

        public string? Description { get; set; } = null!;

        public TExpectedOutput? ExpectedOutput { get; set; }

        public override object[] ToParameterArray()
        {
            _ = SystemUnderTest ?? throw new NullReferenceException(nameof(SystemUnderTest));
            _ = ExpectedOutput ?? throw new NullReferenceException(nameof(SystemUnderTest));
            _ = Description ?? throw new NullReferenceException(nameof(SystemUnderTest));
            var output = new object[3];
            output[0] = SystemUnderTest;
            output[1] = ExpectedOutput;
            output[2] = Description;
            return output;
        }
    }

    public class TheoryData2<TSystemUnderTest, TSystemUnderTest2, TExpectedOutput> : TheoryData
    {
        public TSystemUnderTest? SystemUnderTest { get; set; }

        public TSystemUnderTest2? SystemUnderTest2 { get; set; }

        public string? Description { get; set; }

        public TExpectedOutput? ExpectedOutput { get; set; }

        public override object[] ToParameterArray()
        {
            _ = SystemUnderTest ?? throw new NullReferenceException(nameof(SystemUnderTest));
            _ = SystemUnderTest2 ?? throw new NullReferenceException(nameof(SystemUnderTest2));
            _ = ExpectedOutput ?? throw new NullReferenceException(nameof(SystemUnderTest));
            _ = Description ?? throw new NullReferenceException(nameof(SystemUnderTest));
            var output = new object[4];
            output[0] = SystemUnderTest;
            output[1] = SystemUnderTest2;
            output[2] = ExpectedOutput;
            output[3] = Description;
            return output;
        }
    }

    public class TheoryData3<TSystemUnderTest, TSystemUnderTest2, TSystemUnderTest3, TExpectedOutput> : TheoryData
    {
        public TSystemUnderTest? SystemUnderTest { get; set; }

        public TSystemUnderTest2? SystemUnderTest2 { get; set; }

        public TSystemUnderTest3? SystemUnderTest3 { get; set; }

        public string? Description { get; set; }

        public TExpectedOutput? ExpectedOutput { get; set; }

        public override object[] ToParameterArray()
        {
            var output = new object[5];
            _ = SystemUnderTest ?? throw new NullReferenceException(nameof(SystemUnderTest));
            _ = SystemUnderTest2 ?? throw new NullReferenceException(nameof(SystemUnderTest2));
            _ = SystemUnderTest3 ?? throw new NullReferenceException(nameof(SystemUnderTest3));
            _ = ExpectedOutput ?? throw new NullReferenceException(nameof(SystemUnderTest));
            _ = Description ?? throw new NullReferenceException(nameof(SystemUnderTest));
            output[0] = SystemUnderTest;
            output[1] = SystemUnderTest2;
            output[2] = SystemUnderTest3;
            output[3] = ExpectedOutput;
            output[4] = Description;
            return output;
        }
    }

    public class TheoryFailData<TSystemUnderTest> : TheoryData
    {
        public TSystemUnderTest? SystemUnderTest { get; set; }

        public string? Description { get; set; }

        public override object[] ToParameterArray()
        {
            var output = new object[2];
            _ = SystemUnderTest ?? throw new NullReferenceException(nameof(SystemUnderTest));
            _ = Description ?? throw new NullReferenceException(nameof(SystemUnderTest));
            output[0] = SystemUnderTest;
            output[1] = Description;
            return output;
        }
    }

    public class TheoryFailData2<TSystemUnderTest, TSystemUnderTest2> : TheoryData
    {
        public TSystemUnderTest? SystemUnderTest { get; set; }

        public TSystemUnderTest2? SystemUnderTest2 { get; set; }

        public string? Description { get; set; }

        public override object[] ToParameterArray()
        {
            var output = new object[3];
            _ = SystemUnderTest ?? throw new NullReferenceException(nameof(SystemUnderTest));
            _ = SystemUnderTest2 ?? throw new NullReferenceException(nameof(SystemUnderTest2));
            _ = Description ?? throw new NullReferenceException(nameof(SystemUnderTest));
            output[0] = SystemUnderTest;
            output[1] = SystemUnderTest2;
            output[2] = Description;
            return output;
        }
    }
}

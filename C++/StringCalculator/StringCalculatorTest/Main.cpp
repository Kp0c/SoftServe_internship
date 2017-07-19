#include <StringCalculator.h>
#include <exception>
#include <gtest\gtest.h>

TEST(CalculatorAddTest, EmptyString)
{
    EXPECT_EQ(0, Add(""));
}

TEST(CalculatorAddTest, OneNumber)
{
    EXPECT_EQ(1, Add("1"));
}

TEST(CalculatorAddTest, TwoNumbers)
{
    EXPECT_EQ(3, Add("1,2"));
}

TEST(CalculatorAddTest, FiveNumbers)
{
    EXPECT_EQ(25, Add("5,5,5,5,5"));
}

TEST(CalculatorAddTest, NewLIneInsteadOfComma)
{
    EXPECT_EQ(6, Add("1\n2,3"));
}

TEST(CalculatorAddTest, SupportDifferentDelimiters)
{
    EXPECT_EQ(6, Add("//;\n1;2;3"));
}

TEST(CalculatorAddTest, NegativeShouldThrowException)
{
    try 
    {
        Add("2,-5,-7");
        FAIL();
    }
    catch (std::invalid_argument& ex)
    {
        ASSERT_STREQ("negatives not allowed: -5, -7", ex.what());
    }
}

TEST(CalculatorAddTest, IgnoreMoreThan1000)
{
    EXPECT_EQ(4, Add("//;\n1;1001;3"));
}

TEST(CalculatorAddTest, VariableLengthDelimiter)
{
    EXPECT_EQ(4, Add("//[***]\n1***3"));
}

TEST(CalculatorAddTest, AllowSeveralDelimiters)
{
    EXPECT_EQ(20, Add("//[***][$%][!]\n1***3$%6!10"));
}

int main(int argc, char **argv) {
    ::testing::InitGoogleTest(&argc, argv);
    return RUN_ALL_TESTS();
}

#include <StringCalculator.h>
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

int main(int argc, char **argv) {
    ::testing::InitGoogleTest(&argc, argv);
    return RUN_ALL_TESTS();
}
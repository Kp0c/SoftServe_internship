#include <StringCalculator.h>
#include <Logger.h>
#include <FailLoggerStub.h>
#include <WebServiceMock.h>
#include <exception>
#include <gtest\gtest.h>

class StringCalculatorTest : public ::testing::Test
{
protected:
    virtual void SetUp() {
        logger_ = std::shared_ptr<ILogger>(new Logger);
        logger_->SetOutput(ss_);
        calculator_.SetLogger(logger_);
        calculator_.AddObserver(webService_);
    }

    virtual void TearDown()
    {
        calculator_.RemoveObserver(webService_);
    }

    std::shared_ptr<ILogger> logger_;
    StringCalculator calculator_;
    WebServiceMock webService_;
    std::stringstream ss_;
};

TEST_F(StringCalculatorTest, EmptyString)
{
    EXPECT_EQ(0, calculator_.Add(""));
}

TEST_F(StringCalculatorTest, OneNumber)
{
    EXPECT_EQ(1, calculator_.Add("1"));
}

TEST_F(StringCalculatorTest, TwoNumbers)
{
    EXPECT_EQ(3, calculator_.Add("1,2"));
}

TEST_F(StringCalculatorTest, FiveNumbers)
{
    EXPECT_EQ(25, calculator_.Add("5,5,5,5,5"));
}

TEST_F(StringCalculatorTest, NewLIneInsteadOfComma)
{
    EXPECT_EQ(6, calculator_.Add("1\n2,3"));
}

TEST_F(StringCalculatorTest, SupportDifferentDelimiters)
{
    EXPECT_EQ(6, calculator_.Add("//;\n1;2;3"));
}

TEST_F(StringCalculatorTest, NegativeShouldThrowException)
{
    try 
    {
        calculator_.Add("2,-5,-7");
        FAIL();
    }
    catch (std::invalid_argument& ex)
    {
        EXPECT_STREQ("negatives not allowed: -5, -7", ex.what());
    }
}

TEST_F(StringCalculatorTest, IgnoreMoreThan1000)
{
    EXPECT_EQ(4, calculator_.Add("//;\n1;1001;3"));
}

TEST_F(StringCalculatorTest, VariableLengthDelimiter)
{
    EXPECT_EQ(4, calculator_.Add("//[***]\n1***3"));
}

TEST_F(StringCalculatorTest, AllowSeveralDelimiters)
{
    EXPECT_EQ(20, calculator_.Add("//[***][$%][!]\n1***3$%6!10"));
}

TEST_F(StringCalculatorTest, LoggerTest)
{
    calculator_.Add("//[***][$%][!]\n1***3$%6!10");
    EXPECT_STREQ("20", ss_.str().c_str());
}

TEST_F(StringCalculatorTest, NegativeShouldThrowExceptionToLogger)
{
    try
    {
        calculator_.Add("2,-5,-7");
        FAIL();
    }
    catch (std::invalid_argument&)
    {
        EXPECT_STREQ("negatives not allowed: -5, -7", ss_.str().c_str());
    }
}

TEST_F(StringCalculatorTest, NegativeShouldThrowExceptionToWebService)
{
    std::shared_ptr<ILogger> stubLogger(new FailLoggerStub());
    calculator_.SetLogger(stubLogger);

    try
    {
        calculator_.Add("2,-5,-7");
        FAIL();
    }
    catch (std::invalid_argument&)
    {
        EXPECT_STREQ("Stub exception.", webService_.GetTextOnService().c_str());
    }
}

int main(int argc, char **argv) {
    ::testing::InitGoogleTest(&argc, argv);
    return RUN_ALL_TESTS();
}

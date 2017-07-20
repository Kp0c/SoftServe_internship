#include "TestHelper.h"
#include <ScopedRedirectCout.h>
#include <string>
#include <Windows.h>
#include <gtest\gtest.h>

TEST(scalcTest, RegularAdd)
{
    ASSERT_STREQ("The result is 20\r\nAnother input please: ", StartCalcAndWaitOutput("//[***][$%][!]\\n1***3$%6!10").c_str());
}

TEST(scalcTest, AddSeveralArguments)
{
    ASSERT_STREQ("The result is 10\r\nAnother input please: The result is 3\r\nAnother input please: ", StartCalcAndWaitOutput("5,5", { "//;\\n1;2", "\n" }).c_str());
}

int main(int argc, char **argv) {
     ::testing::InitGoogleTest(&argc, argv);
     return RUN_ALL_TESTS();
}
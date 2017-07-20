#pragma once
#include <string>
#include <iostream>
#include <sstream>  
#include <vector>
#include <Windows.h>

constexpr short BUFSIZE = 1024;

std::string ReadFromPipe(HANDLE pipe);
void WriteToPipe(HANDLE pipe, std::string& msg);

std::string StartCalcAndWaitOutput(std::string arguments, std::vector<std::string>& inputs = std::vector<std::string>{ "\n" })
{
    SECURITY_ATTRIBUTES securityAttributes;
    securityAttributes.nLength = sizeof(SECURITY_ATTRIBUTES);
    securityAttributes.bInheritHandle = TRUE;
    securityAttributes.lpSecurityDescriptor = NULL;

    HANDLE childPipeOutRead = NULL;
    HANDLE childPipeOutWrite = NULL;
    HANDLE childPipeInRead = NULL;
    HANDLE childPipeInWrite = NULL;

    CreatePipe(&childPipeOutRead, &childPipeOutWrite, &securityAttributes, 0);
    SetHandleInformation(childPipeOutRead, HANDLE_FLAG_INHERIT, 0);

    CreatePipe(&childPipeInRead, &childPipeInWrite, &securityAttributes, 0);
    SetHandleInformation(childPipeInWrite, HANDLE_FLAG_INHERIT, 0);

    PROCESS_INFORMATION processInformation;
    ZeroMemory(&processInformation, sizeof(PROCESS_INFORMATION));

    STARTUPINFOA startInfo;
    ZeroMemory(&startInfo, sizeof(STARTUPINFOA));
    startInfo.cb = sizeof(STARTUPINFOA);
    startInfo.hStdError = childPipeOutWrite;
    startInfo.hStdInput = childPipeInRead;
    startInfo.hStdOutput = childPipeOutWrite;
    startInfo.dwFlags |= STARTF_USESTDHANDLES;

    CreateProcessA(NULL, (LPSTR)("scalc.exe " + arguments).c_str(), NULL, NULL, TRUE, 0, NULL, NULL, &startInfo, &processInformation);

    for (std::string input : inputs)
    {
        WriteToPipe(childPipeInWrite, input);
    }

    CloseHandle(childPipeInWrite);

    WaitForSingleObject(processInformation.hProcess, INFINITE);

    CloseHandle(childPipeOutWrite);
    CloseHandle(childPipeInRead);
    CloseHandle(processInformation.hProcess);
    CloseHandle(processInformation.hThread);

    return ReadFromPipe(childPipeOutRead);
}

void WriteToPipe(HANDLE pipe, std::string& msg)
{
    WriteFile(pipe, msg.c_str(), msg.size(), NULL, NULL);
}

std::string ReadFromPipe(HANDLE pipe)
{
    DWORD symbolsRead;
    CHAR output[BUFSIZE];

    ReadFile(pipe, output, BUFSIZE, &symbolsRead, NULL);

    std::stringstream ss;
    for (UINT32 i = 0; i < symbolsRead; i++)
    {
        ss << output[i];
    }

    return ss.str();
}
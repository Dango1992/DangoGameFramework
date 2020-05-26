@echo off

%~d0
cd %~dp0

echo %cd%

set SOURCE_FOLDER=%~dp0..\..\ProtoBuf

cd %SOURCE_FOLDER%

cd %~dp0


set SOURCE_FOLDER=..\..\ProtoBuf


set CS_COMPILER_PATH=protoc.exe
set CS_TARGET_PATH=..\..\Assets\GameMain\Scripts\Network\ProtoGen


del %CS_TARGET_PATH%\*.* /f /s /q


for /f "delims=" %%i in ('dir /b "%SOURCE_FOLDER%\*.proto"') do (
    
    ::Éú³É C# ´úÂë
    echo %CS_COMPILER_PATH% %%i --csharp_out=%CS_TARGET_PATH% --proto_path=%SOURCE_FOLDER%
    %CS_COMPILER_PATH% %%i --csharp_out=%CS_TARGET_PATH% --proto_path=%SOURCE_FOLDER%
)

echo gen success

pause
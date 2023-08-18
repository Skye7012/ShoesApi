@echo off
setlocal enabledelayedexpansion

REM Define the file name
set FILE=docker-compose.override.yml

REM Check if the file exists
if not exist %FILE% (
    echo File '%FILE%' not found.
    exit /b 1
)

echo 1. Skip '%FILE%' in Worktree
echo 2. Unskip '%FILE%' in Worktree
echo 3. Check if %FILE% is skipped in Worktree
:menu
REM Prompt the user to choose an action
choice /c 123 /n /m "Choose an action (1/2/3):"

REM Process the user's choice
if errorlevel 3 (
    REM Check if the skip-worktree bit is set
	set "skipped=false"
    for /f "delims=" %%i in ('git ls-files -v "docker-compose.override.yml"') do (
		if "%%i" == "S docker-compose.override.yml" (
			set "skipped=true"
		)
    )
    if "!skipped!" == "true" (
        echo '%FILE%' is skipped.
    ) else (
        echo '%FILE%' is not skipped.
    )
) else if errorlevel 2 (
    git update-index --no-skip-worktree %FILE%
    echo Not skip in Worktree '%FILE%'.
) else if errorlevel 1 (
    git update-index --skip-worktree %FILE%
    echo Skip in Worktree '%FILE%'.
)

REM Loop back to the menu
echo:
goto menu

cmd /k

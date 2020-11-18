#!/bin/bash
if [ "$OSTYPE" == "msys" -o "$OSTYPE" == "cygwin" ] ; then
	echo -e "\e[37m[Building executable...]\e[2m"
	dotnet publish ./src/Rubik.csproj -r win-x86 -c Release -o ./ -p:PublishSingleFile=True --self-contained True --nologo
	echo -e "\e[1;92m[Build complete!]\e[0m"
	echo -e "\e[93m[You probably should use PowerShell to run executable, or you will likely get errors.]\e[0m"
	read -n 1 -s -r -p "Press any key to continue..."
elif [ "$OSTYPE" == "linux-gnu"* ] ; then
	echo -e "\e[37m[Building executable...]\e[2m"
	dotnet publish ./src/Rubik.csproj -r linux-x64 -c Release -o ./ -p:PublishSingleFile=True --self-contained True --nologo
	echo -e "\e[1;92m[Build complete!]\e[0m"
elif [ "$OSTYPE" == "darwin"* ] ; then
	echo -e "\e[37m[Building executable...]\e[2m"
	dotnet publish ./src/Rubik.csproj -r osx-x64 -c Release -o ./ -p:PublishSingleFile=True --self-contained True --nologo
	echo -e "\e[1;92m[Build complete!]\e[0m"
else
	echo "Can't recognize OSTYPE. Try to build manually or download already build executable!"
fi
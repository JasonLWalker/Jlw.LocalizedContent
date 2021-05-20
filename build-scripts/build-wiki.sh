#!/bin/bash

while getopts p:d:b: flag
do
    case "${flag}" in
        p) packageName=${OPTARG};;
        d) workingDir=${OPTARG};;
        b) buildType==${OPTARG};;

    esac
done

if [ ! $buildType ]; then 
    export buildType="Release"
fi

if [ ! $packageName ]; then 
    export packageName="$(pwd | sed 's/.*\/\([^\/]*\)$/\1/')"
fi

if [ ! $workingDir ]; then 
    export workingDir="$(pwd)"
fi


if [ $GITHUB_ENV ]; then 
    echo "PKGNAME=$packageName" >> $GITHUB_ENV
    echo "PKGVERSION=$packageVersion" >> $GITHUB_ENV
    echo "BUILDTYPE=$buildType" >> $GITHUB_ENV
fi

# Install dependencies
dotnet tool install -g XMLDoc2Markdown

dotnet restore

# Build with dotnet
dotnet build --version-suffix=$versionSuffix --configuration $buildType --no-restore

dotnet publish --no-build --configuration $buildType --verbosity normal "${packageName}"

xmldoc2md ./$packageName/bin/$buildType/netstandard2.0/publish/$packageName.dll ./Help --github-pages --back-button --index-page-name home


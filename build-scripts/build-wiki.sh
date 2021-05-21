#!/bin/bash

# adapted from the github action https://github.com/Andrew-Chen-Wang/github-wiki-action/

while getopts p:d:b:u:e:t:r: flag
do
    case "${flag}" in
        p) packageName=${OPTARG};;
        d) workingDir=${OPTARG};;
        b) buildType=${OPTARG};;
        u) githubUser=${OPTARG};;
        e) githubEmail=${OPTARG};;
        t) githubToken=${OPTARG};;
        r) repo=${OPTARG};;
    esac
done

if [ ! $buildType ]; then 
    export buildType="Release"
fi

if [ ! $packageName ]; then 
    export packageName="$(pwd | sed 's/.*\/\([^\/]*\)$/\1/')"
fi

if [ ! $workingDir ]; then 
    export workingDir="$(pwd)/Help/"
fi

if [ ! $repo ]; then
    export repo="$(pwd | sed 's/.*\/\([^\/]*\)$/\1/')"
fi



if [ $GITHUB_ENV ]; then 
    echo "PKGNAME=$packageName" >> $GITHUB_ENV
    echo "PKGVERSION=$packageVersion" >> $GITHUB_ENV
    echo "BUILDTYPE=$buildType" >> $GITHUB_ENV
fi

# Install dependencies
dotnet tool install -g XMLDoc2Markdown

dotnet publish --configuration $buildType  --verbosity normal $packageName

xmldoc2md ./$packageName/bin/$buildType/netstandard2.0/publish/$packageName.dll ./Help --github-pages --back-button --index-page-name home


TEMP_CLONE_FOLDER="temp_wiki_$GITHUB_SHA"
#TEMP_EXCLUDED_FILE="temp_wiki_excluded_$GITHUB_SHA.txt"

echo "Configuring wiki git..."
mkdir $TEMP_CLONE_FOLDER
cd $TEMP_CLONE_FOLDER
git init



# Setup credentials
echo "Setting up git credentials"
git config user.name $githubUser
git config user.email $githubEmail


echo "Pulling current wiki from repo"
git pull https://$githubToken@github.com/$githubUser/$repo.wiki.git

cd ..


echo "Copying from source to temp folder"
rsync -av --delete $workingDir $TEMP_CLONE_FOLDER/ --exclude .git

echo "Pushing to Wiki"
cd $TEMP_CLONE_FOLDER

message=$(git log -1 --format=%B)

git add .
git commit -m "$message"
git push --set-upstream https://${githubUser}:${githubToken}@github.com/$githubUser/$repo.wiki.git master

cd ..
rm -rf $TEMP_CLONE_FOLDER
#rm -f $TEMP_EXCLUDED_FILE

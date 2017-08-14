#!/bin/sh

if [ ! -d ".deployment" ]; then
  echo "Cloning deployment branch"
  git clone -b deployment https://github.com/infosupport/celebscan-api .deployment
fi

echo "Cleaning up deployment folder"
cd .deployment && rm -rf * && cd ..

echo "Publishing binaries"
cd src/Celebscan.Service/ && dotnet publish -c Release && cd ../..
cp -r src/Celebscan.Service/bin/Release/netcoreapp1.1/publish/*  .deployment/
rm -rf .deployment/.gitignore
rm -rf .deployment/.travis.yml
rm -rf .deployment/.github

echo "Pushing binaries"
cd .deployment && git add . && git commit -am "New deployment" && git push origin deployment

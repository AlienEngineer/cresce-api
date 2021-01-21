#!/bin/bash

function getVersion {
input="Cresce.Api/appsettings.json"
while IFS= read -r line
do
  if [[ $line == *"Version"* ]]; then
    echo "$line"
    return
  fi

done < "$input"
}

function increment_version
{
  declare -a part=( ${1//\./ } )
  declare    new
  declare -i carry=1

  new=$((part[2]+1))
  part[2]=${new}

  new="${part[*]}"
  echo -e "${new// /.}"
}

version=$(echo $(getVersion) | sed 's/"Version": "//g' | sed 's/",//g')


newVersion=$(echo $(increment_version $version))

echo "$version > $newVersion"

sed "s/$version/$newVersion/" Cresce.Api/appsettings.json > changed.txt && mv changed.txt Cresce.Api/appsettings.json

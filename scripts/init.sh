#!/bin/bash
echo "Creating openshift project!"
read -p 'Project: ' project
oc new-project $project
oc new-build --name=$project dotnet:latest --binary=true
oc start-build $project --from-dir=../. --follow
oc new-app $project

echo 'Application successfully deployed. Please, add postgres database and set enviroment variable for connection string'
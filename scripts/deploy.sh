#!/bin/bash
echo "Starting deploy to openshift..."
oc start-build uschedule --from-dir=../. --follow
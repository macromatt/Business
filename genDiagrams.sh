#!/bin/sh
# The MIT License (MIT) Copyright (c) 2014 Stephen A Jazdzewski

mkdir -p diagrams

ARGS="--gutter=50 --db=XML"

INDIVIDUAL="Given Family Person Individual Company Word"

# Include invalid refrences for display purposes only
cat schema.xml | sed '/invalid/ {s/<comments invalid="">//; s/<\/comments>//}' > schema.xml.invalid

scripts/extractTable.pl schema.xml.invalid $INDIVIDUAL >./zot.xml
sqlt-diagram --title "Individual People and Company Events" $ARGS -c 2 -o diagrams/individual.png ./zot.xml

# Remove temporary files
rm zot.xml
rm schema.xml.invalid
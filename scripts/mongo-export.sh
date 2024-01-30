#!/bin/bash

mongoexport \
    --username=rootuser \
    --password=rootpassword  \
    --authenticationDatabase=admin \
    --db=household-budget-development \
    --port=27017 \
    --collection=transaction \
    --type=json \
    --fields=category.name,category.subcategory.name,payment.total \
    --out=/bkp/transactions.json


mongoexport \
    --username=rootuser \
    --password=rootpassword  \
    --authenticationDatabase=admin \
    --db=household-budget-development \
    --port=27017 \
    --collection=subcategory \
    --type=json \
    --fields=name,category.name\
    --out=/bkp/subcategories.json
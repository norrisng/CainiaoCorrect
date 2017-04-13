# Changelog #

## 1.4.0 ##

Released **2017-04-13**

* **Feature:** Auto-correction now removes "+" in all <phone> and <mobile> fields
* **Feature:** Auto-correction now removes "#" in <name> and corresponding <categoryName>
* **Bugfix:** Allow more time for newly-submitted shipments to propogate through DHL backend

## 1.3.2 ##

Released **2017-04-13**

* **Bugfix:** "~" auto-correction wasn't working at all

## 1.3.1 ##

Released **2017-04-12**

* **Feature:** Auto-correction now replaces "~" with a space
* **Bugfix:** Identical shipment IDs are no longer verified multiple times


## 1.3.0 ##

Released **2017-04-07**

* **Feature:** Successfully retrieved Shipment IDs are now checked online to see if they were submitted
* **Bugfix:** XML is no longer retrieved if shipment ID doesn't exist

## 1.2.1 ##

Released **2017-04-06**

 * **Feature:** Program now warns user if `cainiao_Orderpush.csv` doesn't exist
 * **Bugfix:** Duplicate shipment IDs are now only processed once 

## 1.2.0 ##

Released **2017-04-05** (initial GitHub commit)

 * **Feature:** "Unit price must be greater than 0" errors are now auto-corrected before being copied to the clipboard
 * **Feature:** "IC"/"Integrated Circuit" errors are now auto-corrected before being copied to the clipboard

## 1.1.0 ##

Released **2017-03-31**

 * **Feature:** Multiple shipment IDs can now be looked up without exiting the program
 * **Feature:** Program now searches through multiple CSV files, as long as each is named "cainiao_orderpush.csv" or "cainiao_orderpush?.csv" (where ? represents any single character)

## 1.0.0 ##

Released **2017-03-29**

 * Initial release
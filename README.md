# CainiaoCorrect #

CainiaoCorrect is a simple program that can retrieve the RequestFileContent for a particular shipment ID from multiple DHL eCommerce AP Portal Order/Tracking reports. It even removes the HTTP header and performs basic error correction, allowing for immediate copy-and-pasting elsewhere for further editing if needed.

It also checks if all shipment IDs that were successfully retrieved were submitted to DHLeC. This can be useful as a "final check". 

As of this current version, basic error correction includes the following:

Location 							| Original 					| Replacement
------------------------------------|---------------------------|---------------
`<declarePrice>` and `<quantity>` 	| Less than or equal to 99 	| 100
`<name>` and `<categoryName>` 		| IC 						| Integrated Circuit
`<name>` and `<categoryName>` 		| `#` 						| (remove)
`<phone>`, `<mobile>`				| `+` 						| (remove)
Everywhere 							| `~` 						| (space)

This program was designed to retrieve Cainiao shipment requests, but most likely works for shipments submitted by any customer.

## Installation ##

No installation is requred. Simply download the latest release under "releases" at the top of the GitHub project page, then extract the *.zip to any location.

The following text files are required for getDataDigest endpoint functionality. This hasn't been implemented yet, however:
 * `api/dataDigestEndpoint.txt` - this file should contain the DataDigest API endpoint URI
 * `api/dataDigestKey.txt` - this file should contain the `secretKey` as required by the DataDigest server

## How to use ##

1. Download all Order/Tracking reports, and place it in the same directory as CainiaoCorrect. Files MUST be named `cainiao_Orderpush.csv`, or `cainiao_Orderpush?.csv` (where `?` equals any SINGLE character). If a combined file already exists (this combined file is named `cainiao_Orderpush_combined.csv`), it will be overwritten.

2. Open CainiaoCorrect.

3. Paste Shipment ID when prompted. `Ctrl-V` probably won't work, so delete the `^V` that appears (that is, if you've already tried using `Ctrl-V`), click on the program icon in the top-left corner of the window, and then click **Edit** > **Paste**.

4. Press Enter to proceed. If the shipment ID is present, the XML will automatically be copied to your clipboard, which can then be pasted anywhere (including Postman, if no further corrections are necessary). 

5. Repeat steps (3) and (4) to look up another shipment ID. This can be repeated as many times as desired.

6. To quit, leave the input empty, then press Enter. The program will now check and see if previously-retrieved shipments were successfully submitted.

## Source code ##

Available on GitHub: https://github.com/norrisng/CainiaoCorrect

## Third-party libraries ##

CainiaoCorrect uses the [TinyCsvParser](https://github.com/bytefish/TinyCsvParser) library by [Philipp Wagner](http://www.bytefish.de).
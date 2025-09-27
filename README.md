osushi-query
===

*OsushiQuery* is a fake OSCQuery service that contains the minimum amount of work necessary to communicate with
a very specific version of VRCFaceTracking **5.2.3.0** which was released on Steam.

This does the following:
- A) Opens an HTTP service on a random port:
  - When queried for any URL that ends with `/avatar`, it replies with a pre-determined message (check out `response-avtr.json`).
  - When queried for any other URL, it replies with another pre-determined message (check out `response.json`).
- B) Advertises that port as a service on mDNS as `_oscjson._tcp` with the instance name `VRChat-Client-XXXXXX`
  where XXXXXX is a random number between 100000 and 999999.
- C) Queries for `_oscjson._tcp` once when the service starts.

A and B are sufficient for VRCFaceTracking to detect our program if the OsushiQuery service is already running when VRCFaceTracking starts.

C is needed to handle the case where VRCFaceTracking is already running before our program starts the OsushiQuery service.

The HTTP service completely violates the OSCQuery protocol and is not intended to be read by any other program other than VRCFaceTracking.

---

For completeness, VRCFaceTracking will in addition open every JSON file located in any of the `AppData/LocalLow/VRChat/vrchat/OSC/any_user/Avatars/`
folders and try to find the first file which `id` is equal to the value contained within the JSON path of `CONTENTS.change.VALUE[0]`
of the HTTP response provided by OsushiQuery at when the queried URL is `/avatar`.

VRCFaceTracking uses that to resolve the name of the avatar which will be displayed in the UI (although, it will not crash if it can't
find it and continue to work normally).

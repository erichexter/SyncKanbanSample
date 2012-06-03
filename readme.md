Sample mutli-browser screen sync
================================
Using ASP.Net / SignalR / KnockoutJS / JQuery UI Sortable
---------------------------------------------------------

To see a working sample of the application go to : http://syncsample.apphb.com

The key points to take away from this sample is that knockoutjs deals with childcollections in a 
very specific way. Each child collection needs to be wrapped by a knockout object in order to bind
your child arrays to the ui templates.  So, when you send an object that has child arrays down to
the client using singalR, you need to map from the server types, to an identical structure that 
has the child arrays wrapped with the knockout observable array object.





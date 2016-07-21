# WMCDuplicateRemover
An application that removes recordings that Windows Media Center has previously recorded.

There's quite a bit of unused code that I need to go in and delete.

The gist of it is though it uses http://mc2xml.hosterbox.net/ to get an Electronic Programming Guide (EPG) from a source.
Once it's downloaded the EPG it finds all the scheduled recordings from the event logs on your PC.
After it has all that info it checks to see what Windows Media Center(WMC) has marked as recorded.
With that information it goes ahead and cancels all the recordings that appear to be duplicates.

I've been using this for over a year now and it seems to work pretty well. I'm just finally getting around to open sourcing it.



As for the EPG source I pay for and use schedules direct http://www.schedulesdirect.org/.
There are other places to get it from that mc2xml app but Schedules Direct seemed to be much more accurate and it's only $25/Year.

There are some issues that I'd like to work on at some point. I've added them and hopefully this will be more useful than just for me.

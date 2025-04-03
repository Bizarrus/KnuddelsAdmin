@ECHO OFF

set "PLUGIN_PATH=%CD%"
set "PLUGIN_EXE=%PLUGIN_PATH%/source/KnuddelsAdmin/bin/Release/net9.0-windows7.0/KnuddelsAdmin.exe"

"%PLUGIN_EXE%" -port 18618 -pluginUUID AAAAAABBBBBBCCCCCCEEEEEEFFFFFF -registerEvent registerPlugin -info {"application":{"font":"HarmonyOS Sans","language":"de","platform":"windows","platformVersion":"10.0.26100","version":"3.10.187.1230"},"colors":{"buttonMouseOverBackgroundColor":"#464646FF","buttonPressedBackgroundColor":"#303030FF","buttonPressedBorderColor":"#646464FF","buttonPressedTextColor":"#969696FF","highlightColor":"#0078FFFF"},"devicePixelRatio":1,"devices":[{"id":"18XK1JK02PZ36GF54QPF04DY01821WVY","name":"StreamDock[293S]","size":{"columns":6,"rows":3},"type":0}],"plugin":{"uuid":"com.github.bizarrus.knuddelsadmin","name":"LOL","version":"1.0"}}
pause

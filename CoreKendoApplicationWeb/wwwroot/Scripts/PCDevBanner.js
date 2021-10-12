/**
   Usage: Call setEnvironmentBanner in document ready.
   Note: If the site has multiple pages with multiple document ready calls, this will need to be repeated.

   Example:

   $(document).ready(function () {
    setEnvironmentBanner();
});
 */

function setEnvironmentBanner() {
    var hostName = window.location.hostname;

    var envNameToDisplay = "";
    var showEnv = false;

    if (hostName.indexOf("localhost") != -1) {
        envNameToDisplay = "LOCAL DEV";
        showEnv = true;
    } else if (hostName.indexOf("DEV-SERVER-HOSTNAME") != -1) {
        envNameToDisplay = "DEV";
        showEnv = true;
    } else if (hostName.indexOf("TEST-SERVER-HOSTNAME") != -1) {
        envNameToDisplay = "TEST";
        showEnv = true;
    }

    if (showEnv) {
        $('#environmentDescriptorWrapper').removeClass('hidebanner');
        $('#environmentDescriptor').text(envNameToDisplay);
    }
}
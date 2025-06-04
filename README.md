    	DeviceManager:
    Udviklet til .Net og BlazorWebApp

    DeviceManager skal holde en liste af alle de devices som er kendte. Device har nogle attributter som er sepereret ud for ikke at opdatere hele objektet ved hvert kald.

KRAV:
-GET status for 10-35 enheder samtidig via API
Status skal sendes til GRAFANA
-GET CONF for hver enhed
BEGREBER:
Device
Read current state (1/all)
Read current conf (1)

DESIGN:
Single responsebility
APIManager til at håndtere call
GETS og PUTS
Returnere DeviceStatus og DeviceConfig
Device, DeviceStatus og DeviceConfig

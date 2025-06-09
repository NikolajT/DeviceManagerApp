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

ANALYSE:
Manager til at holde devices
Device som objekt

DESIGN:
Single Responsibility
APIManager til at håndtere call
GETS og PUTS
Returnere DeviceStatus og DeviceConfig
Device, DeviceStatus og DeviceConfig

Teknisk:
Appen
Kør med .NET 8 Gå til https://localhost:5001/metrics (eller den port appen starter på). Opserver:

        "HELP device_streaming_count Number of devices currently streaming
        TYPE device_streaming_count gauge
        device_streaming_count 2"

Port i prometheus.yml og localhost:<port> skal stemme overens. Jeg kørte på 7219..

Dataen kommer fra testdata i Program.cs
Prometheus (databroker?)
Kræver docker kører (Og at du står i prometheusmappen i DeviceManagerApp)
docker run -p 9090:9090 \
 -v $(pwd)/prometheus.yml:/etc/prometheus/prometheus.yml \
 prom/prometheus

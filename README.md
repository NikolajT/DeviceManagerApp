		DeviceManger:
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
	Device, DeviceStatus og DeviceConfig
	
	
	

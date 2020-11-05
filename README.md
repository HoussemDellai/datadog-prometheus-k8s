# datadog-prometheus-k8s  

This demo will walk you through:  
1) Installing Datadog into Kubernetes  
2) Collecting default metrics  
3) Collecting application specific metrics  
4) Collecting custom Prometheus metrics  

Install Datadog into Kubernetes using Helm chart

```bash
API_KEY="<YOUR DATADOG API KEY>"

helm repo add datadog https://helm.datadoghq.com

helm install datadog `
             --set datadog.site='datadoghq.com' `
             --set datadog.apiKey=$API_KEY `
             --set datadog.apm.enabled=true `
             datadog/datadog
```

View metrics on Datadog dashboard
Deploy sample app expposing prometheus metrics on /metrics

```bash 
kubectl apply -f app-deployment.yaml
```

Show Prometheus Nuget package config in dotnetcore-app/Startup.cs  
View the metrics on /metrics  
Now we want to collect these application metrics using Datadog  

Configure Datadog to collect application metrics using datadog-values.yaml file  

```bash
helm upgrade datadog -f datadog-values.yaml `
             --set datadog.site='datadoghq.com' `
             --set datadog.apiKey=$API_KEY `
             --set datadog.apm.enabled=true `
             datadog/datadog
```

View application specific metrics on Datadog dashboard -> Metrics -> Explorer  
Creating custom metrics like page views counter in app code, in  dotnetcore-app/Controllers/HomeController.cs    
Viewing the custom metric in Datadog dashboard  
# Install Datadog into Kubernetes using Helm chart

API_KEY="<YOUR DATADOG API KEY>"

helm repo add datadog https://helm.datadoghq.com

helm install datadog `
             --set datadog.site='datadoghq.com' `
             --set datadog.apiKey=$API_KEY `
             --set datadog.apm.enabled=true `
             datadog/datadog

# View metrics on Datadog dashboard

# Deploy sample app expposing prometheus metrics on /metrics
kubectl apply -f app-deployment.yaml

# Show Prometheus Nuget package config and View the metrics on /metrics

# Now we want to collect these application metrics using Datadog

# Configure Datadog to collect application metrics
# using datadog-values.yaml file

helm upgrade datadog -f datadog-values.yaml `
             --set datadog.site='datadoghq.com' `
             --set datadog.apiKey=$API_KEY `
             --set datadog.apm.enabled=true `
             datadog/datadog

# View custom metrics on Datadog dashboard -> Metrics -> Explorer

# Creating custom metrics like page views counter in app code

# Viewing the custom metric in Datadog dashboard

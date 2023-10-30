pipeline {
  agent any
  
  environment {
    ASPNETCORE_ENVIRONMENT = 'Production'
  }
  
  stages {
    stage('Build and Test') {
      steps {
        sh 'dotnet build --configuration Release'
        sh 'dotnet test --configuration Release'
      }
    }
    
    stage('Publish') {
      steps {
        sh 'dotnet publish --configuration Release --output publish'
      }
    }
    
    //stage('Deploy') {
    //  steps {
    //    withCredentials([azureServicePrincipal('azure-credentials')]) {
    //      azureWebAppPublish appName: 'WebApiTFApp', filePath: 'publish', resourceGroup: 'DESWebApiTF', slotName: 'production', subscriptionId: credentials('azure-subscription-id'), tenantId: credentials('azure-tenant-id')
    //    }
    //  }
    //}
  }
}
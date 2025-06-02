pipeline {
    agent any
    environment {
        FRONTEND_IMAGE = "mahadev1667/employeemgmnt-frontend:v2.5"
        BACKEND_IMAGE = "mahadev1667/employeemgmnt-backend:v2.5"
    }
    stages {
        stage('Clone Repository') {
            steps {
                git 'https://github.com/Mahadeopimpalkar16/EmploeeManagementUsingDeployments.git'
            }
        }
        stage('Build & Push Frontend Image') {
            steps {
                sh 'docker build -t $FRONTEND_IMAGE Frontend/'
                withDockerRegistry([ credentialsId: 'dockerhub-credentials', url: '' ]) {
                    sh 'docker push $FRONTEND_IMAGE'
                }
            }
        }
        stage('Build & Push Backend Image') {
            steps {
                sh 'docker build -t $BACKEND_IMAGE Backend/'
                withDockerRegistry([ credentialsId: 'dockerhub-credentials', url: '' ]) {
                    sh 'docker push $BACKEND_IMAGE'
                }
            }
        }
        stage('Deploy to Kubernetes') {
            steps {
                sh 'kubectl apply -f Kubernates/deployment.yaml'
                sh 'kubectl apply -f Kubernates/service.yaml'
            }
        }
        stage('Restart Kubernetes Pods') {
            steps {
                sh 'kubectl rollout restart deployment employeemgmnt-backend-deployment'
                sh 'kubectl rollout restart deployment employee-frontend'
            }
        }
    }
}

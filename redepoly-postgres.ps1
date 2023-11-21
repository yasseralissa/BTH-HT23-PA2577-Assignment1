kubectl apply -f ./postgres/db-configmap.yaml
kubectl apply -f ./postgres/db-persistent-volume.yaml
kubectl apply -f ./postgres/db-volume-claim.yaml
kubectl apply -f ./postgres/db-statefulSet.yaml
kubectl apply -f ./postgres/db-deployment.yaml
kubectl apply -f ./postgres/db-service.yaml
#kubectl apply -f ./postgres/ingress.yaml

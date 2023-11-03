kubectl delete -f .\services\OrderService\service.yml
kubectl delete -f .\services\OrderService\deployment.yml

kubectl apply -f .\services\OrderService\service.yml
kubectl apply -f .\services\OrderService\deployment.yml
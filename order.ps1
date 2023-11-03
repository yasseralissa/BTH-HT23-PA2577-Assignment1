docker build -t order-service:1.0.6 -t order-service:latest .\services\OrderService

docker tag order-service:latest yaseralissa/order-service:latest

docker push yaseralissa/order-service:latest

kubectl delete -f .\services\OrderService\service.yml
kubectl delete -f .\services\OrderService\deployment.yml

kubectl apply -f .\services\OrderService\deployment.yml
kubectl apply -f .\services\OrderService\service.yml



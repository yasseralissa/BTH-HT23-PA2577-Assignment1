docker build -t product-service:1.0.6 -t product-service:latest .\services\ProductService

docker tag product-service:latest yaseralissa/product-service:latest

docker push yaseralissa/product-service:latest

kubectl delete -f .\services\ProductService\service.yml
kubectl delete -f .\services\ProductService\deployment.yml

kubectl apply -f .\services\ProductService\deployment.yml
kubectl apply -f .\services\ProductService\service.yml
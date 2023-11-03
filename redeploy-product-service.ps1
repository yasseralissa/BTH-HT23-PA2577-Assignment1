kubectl delete -f .\services\ProductService\service.yml
kubectl delete -f .\services\ProductService\deployment.yml

kubectl apply -f .\services\ProductService\service.yml
kubectl apply -f .\services\ProductService\deployment.yml
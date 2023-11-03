kubectl delete -f ingress.yaml
kubectl delete -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.1.3/deploy/static/provider/baremetal/deploy.yaml


kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.1.3/deploy/static/provider/baremetal/deploy.yaml

kubectl get pods -n ingress-nginx


kubectl get services -n ingress-nginx


kubectl -n ingress-nginx get ingressclasses


kubectl -n ingress-nginx annotate ingressclasses nginx ingressclass.kubernetes.io/is-default-class="true"


kubectl delete -A ValidatingWebhookConfiguration ingress-nginx-admission

kubectl apply -f ingress.yaml

kubectl get svc -n ingress-nginx
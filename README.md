# Open Telemetry Distributed Tracing on .Net 6

In this PoC project, we talked about how to use the Open Telemetry Tracing library in .Net 6. Our goal was to manage the distributed tracing of two services called Api1 and Api2 that communicate with each other via http/grpc. We used both Jaeger and Zipkin as UI.

For detailed custom tracing, we can trace some service methods in detail by writing Interceptor with Castle Windsor Dynamic Proxy. 
   (You can use open telemetry for services that do not have its own integration package and are really needed. Other than that, I wouldn't recommend much. It may be necessary to avoid, especially if there is a need for performance.)

## Questions

You can find answers them in this project;

- How can I implement Open Telemetry?
- What is distribution tracing and how is it implemented?
- How Open Telemetry Collector exports trace data. Which UI tools are used ?
- How can I implement Zipkin and Jaeger ?


## Getting Started

### Dependencies

* All you need is docker. Docker must be installed on your computer.

### Installing

* You can pull the project to your local with this code;
```
git clone https://github.com/arabamcom/OpenTelemetryTracingDotnet.git
```

### Executing program

* In the command line after entering the project directory, you can execute this code block;
```
docker-compose up -d
```

**Jaeger UI:** http://localhost:16686/search

**Zipkin UI:** http://localhost:9411/zipkin/

## Help

For any problem, you can contact [ismail.deniz@arabam.com](mailto:ismail.deniz@arabam.com) or twitter.

## Authors

Contributors names and contact info

ex. [@ismaildenizzz](https://twitter.com/ismaildenizzz)

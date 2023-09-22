// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/shopping_cart.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace GrpcShoppingCartService {
  public static partial class GrpcShoppingCartService
  {
    static readonly string __ServiceName = "GrpcShoppingCartService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcShoppingCartService.GrpcShoppingCartDto> __Marshaller_GrpcShoppingCartDto = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcShoppingCartService.GrpcShoppingCartDto.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.StringValue> __Marshaller_google_protobuf_StringValue = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.StringValue.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcShoppingCartService.GrpcAddShoppingCartItemCommandDto> __Marshaller_GrpcAddShoppingCartItemCommandDto = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcShoppingCartService.GrpcAddShoppingCartItemCommandDto.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.Empty.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcShoppingCartService.GrpcUpdateShoppingCartItemCommandDto> __Marshaller_GrpcUpdateShoppingCartItemCommandDto = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcShoppingCartService.GrpcUpdateShoppingCartItemCommandDto.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcShoppingCartService.GrpcDeleteShoppingCartItemCommandDto> __Marshaller_GrpcDeleteShoppingCartItemCommandDto = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcShoppingCartService.GrpcDeleteShoppingCartItemCommandDto.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcShoppingCartService.OrderDetailsRequest> __Marshaller_OrderDetailsRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcShoppingCartService.OrderDetailsRequest.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcShoppingCartService.GrpcShoppingCartDto, global::Google.Protobuf.WellKnownTypes.StringValue> __Method_AddShoppingCart = new grpc::Method<global::GrpcShoppingCartService.GrpcShoppingCartDto, global::Google.Protobuf.WellKnownTypes.StringValue>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddShoppingCart",
        __Marshaller_GrpcShoppingCartDto,
        __Marshaller_google_protobuf_StringValue);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcShoppingCartService.GrpcAddShoppingCartItemCommandDto, global::Google.Protobuf.WellKnownTypes.Empty> __Method_AddShoppingItem = new grpc::Method<global::GrpcShoppingCartService.GrpcAddShoppingCartItemCommandDto, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddShoppingItem",
        __Marshaller_GrpcAddShoppingCartItemCommandDto,
        __Marshaller_google_protobuf_Empty);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcShoppingCartService.GrpcUpdateShoppingCartItemCommandDto, global::Google.Protobuf.WellKnownTypes.Empty> __Method_UpdateShoppingItem = new grpc::Method<global::GrpcShoppingCartService.GrpcUpdateShoppingCartItemCommandDto, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UpdateShoppingItem",
        __Marshaller_GrpcUpdateShoppingCartItemCommandDto,
        __Marshaller_google_protobuf_Empty);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcShoppingCartService.GrpcDeleteShoppingCartItemCommandDto, global::Google.Protobuf.WellKnownTypes.Empty> __Method_DeleteShoppingItem = new grpc::Method<global::GrpcShoppingCartService.GrpcDeleteShoppingCartItemCommandDto, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "DeleteShoppingItem",
        __Marshaller_GrpcDeleteShoppingCartItemCommandDto,
        __Marshaller_google_protobuf_Empty);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcShoppingCartService.GrpcShoppingCartDto, global::Google.Protobuf.WellKnownTypes.Empty> __Method_CheckoutShoppingCart = new grpc::Method<global::GrpcShoppingCartService.GrpcShoppingCartDto, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CheckoutShoppingCart",
        __Marshaller_GrpcShoppingCartDto,
        __Marshaller_google_protobuf_Empty);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcShoppingCartService.OrderDetailsRequest, global::GrpcShoppingCartService.GrpcShoppingCartDto> __Method_GetOrderDetails = new grpc::Method<global::GrpcShoppingCartService.OrderDetailsRequest, global::GrpcShoppingCartService.GrpcShoppingCartDto>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetOrderDetails",
        __Marshaller_OrderDetailsRequest,
        __Marshaller_GrpcShoppingCartDto);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::GrpcShoppingCartService.ShoppingCartReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of GrpcShoppingCartService</summary>
    [grpc::BindServiceMethod(typeof(GrpcShoppingCartService), "BindService")]
    public abstract partial class GrpcShoppingCartServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.StringValue> AddShoppingCart(global::GrpcShoppingCartService.GrpcShoppingCartDto request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> AddShoppingItem(global::GrpcShoppingCartService.GrpcAddShoppingCartItemCommandDto request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> UpdateShoppingItem(global::GrpcShoppingCartService.GrpcUpdateShoppingCartItemCommandDto request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> DeleteShoppingItem(global::GrpcShoppingCartService.GrpcDeleteShoppingCartItemCommandDto request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> CheckoutShoppingCart(global::GrpcShoppingCartService.GrpcShoppingCartDto request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::GrpcShoppingCartService.GrpcShoppingCartDto> GetOrderDetails(global::GrpcShoppingCartService.OrderDetailsRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(GrpcShoppingCartServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_AddShoppingCart, serviceImpl.AddShoppingCart)
          .AddMethod(__Method_AddShoppingItem, serviceImpl.AddShoppingItem)
          .AddMethod(__Method_UpdateShoppingItem, serviceImpl.UpdateShoppingItem)
          .AddMethod(__Method_DeleteShoppingItem, serviceImpl.DeleteShoppingItem)
          .AddMethod(__Method_CheckoutShoppingCart, serviceImpl.CheckoutShoppingCart)
          .AddMethod(__Method_GetOrderDetails, serviceImpl.GetOrderDetails).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GrpcShoppingCartServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_AddShoppingCart, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcShoppingCartService.GrpcShoppingCartDto, global::Google.Protobuf.WellKnownTypes.StringValue>(serviceImpl.AddShoppingCart));
      serviceBinder.AddMethod(__Method_AddShoppingItem, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcShoppingCartService.GrpcAddShoppingCartItemCommandDto, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.AddShoppingItem));
      serviceBinder.AddMethod(__Method_UpdateShoppingItem, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcShoppingCartService.GrpcUpdateShoppingCartItemCommandDto, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.UpdateShoppingItem));
      serviceBinder.AddMethod(__Method_DeleteShoppingItem, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcShoppingCartService.GrpcDeleteShoppingCartItemCommandDto, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.DeleteShoppingItem));
      serviceBinder.AddMethod(__Method_CheckoutShoppingCart, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcShoppingCartService.GrpcShoppingCartDto, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.CheckoutShoppingCart));
      serviceBinder.AddMethod(__Method_GetOrderDetails, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcShoppingCartService.OrderDetailsRequest, global::GrpcShoppingCartService.GrpcShoppingCartDto>(serviceImpl.GetOrderDetails));
    }

  }
}
#endregion
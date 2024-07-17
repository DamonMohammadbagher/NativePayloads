# NativePayload_Call4

### NativePayload_Call4 Emit Call Method + Indirect Invoke C# Method

"Emit(Opcodes.Call)" instead "Emit(Opcodes.Jmp)" 

in C# you can use Emit(Opcodes.jmp,TargetMethod) in your codes without writing any asm bytes in code to jump to pointer of TargetMethod or (MethodInfo) to run in-memory via Emit(Opcodes.jmp, method) in system.reflection namespace ;D
yeah you can use this technique to create your own Jump code without write any assembly code in source code ;p lol , "so in this case Csharp Method name [ExecuteInMemory] indirectly called in-memory via jump method without calling c# Method in source code" , i talked about this in my ebook "Bypassing Anti-viruses by C# Programming v2.0" in chapter 3.4 and now you can see source code ;) 

now you can do this method via "Emit(Opcodes.Call)" instead "Emit(Opcodes.Jmp)" , this is new for me maybe is new for you too ;P 

in this picture you can see background of code for Emit.(Opcodes.Call) vs Emit.(Opcodes.Jmp)
 
 ![](https://github.com/DamonMohammadbagher/NativePayloads/blob/main/NativePayload_Call4/NativePayload_Call4.png)

as you can see in the picture C# Method name ExecuteInMemory called Indirectly in memory via Emit.(Opcodes.Call) Technique , 
so this Method is running in-memory without calling in source code ...

Usage: 
    
     NativePayload_Call4.exe 
      
 
<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/DamonMohammadbagher/NativePayload_Call4/"/></a></p>

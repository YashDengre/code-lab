## Proxy Pattern
👉 Proxy = Controlled access to an object

You don’t call the real service directly.
You call a proxy, and proxy decides:
- whether to call real object
- when to call it
- how to call it
- whether to block / cache / secure / lazy load

`Client → Proxy → Real Service`

Key Intent:
`Control Access
NOT enhance behavior (Decorator)
NOT change interface (Adapter)`

🎯 2. When Enterprise Systems Use Proxy

Very real scenarios:

- ✅ Authorization check before service call
- ✅ Lazy loading (EF Core navigation properties)
- ✅ Remote API calls (gRPC/HTTP clients)
- ✅ Caching layer
- ✅ Logging & monitoring
- ✅ Circuit breaker / resilience
- ✅ Expensive object initialization


### Proxy Types
- Core Proxy Types (The Real Foundations)
	
	- ✅ 1. Virtual Proxy (Lazy Initialization)
		- Intent: Delay creation of expensive object until needed.
		- Enterprise Example: EF Core Lazy Loading, Loading large files/images, Expensive ML model initialization, Heavy API clients
		- Concept Flow: `Client → Proxy → creates real object only when required`
		- Example: ` if (_realImage == null)
            _realImage = new RealImage(_file);`
	- ✅ 2. Protection Proxy (Security / Authorization)
		- Intent: Control access based on role or permission.
		- Enterprise Example: RBAC checks, API permission layer, admin operations
		- Example: `if(user.Role != "Admin")
   throw new UnauthorizedAccessException();`
	- ✅ 3. Remote Proxy (Distributed Systems)
		- Intent: Represent an object living on another machine.
		- Enterprise Examples: gRPC client, REST SDK wrapper, Azure SDK clients, WCF proxy
		- Example: `IPaymentService → HttpClientPaymentProxy → Remote API`
	- ✅ 4. Smart Proxy (Lifecycle / Extra Control)
		- Intent: Add monitoring or resource control.
		- Enterprise Examples: Performance tracking, Reference counting, retry / resilience, transaction wrapper
		- Example: `var sw = Stopwatch.StartNew();
await _service.DoWork();
sw.Stop();`
	- ✅ 5. Caching Proxy
		- Some books treat it as Smart Proxy.
		- Some separate it because it’s very common
		- Enterprise Examples: MemoryCache layer, Redis cache wrapper, Response cache proxy
		- Example: `if(cache.TryGet(key, out result))
   return result;`
	🌐 Other Names You May See Online
		- | Name | Reality |
|----------|----------|
| Logging Proxy   | Smart Proxy  |
| Firewall Proxy  | Protection Proxy   |
| Synchronization Proxy   | Smart Proxy  |
| Validation Proxy  | Protection/Smart Proxy   |
| Circuit Breaker Proxy   | Smart Proxy  |
| Rate Limiting Proxy  | Protection Proxy   |
| Lazy Proxy  | Virtual Proxy   |
| Stub / Client Proxy   | Remote Proxy  |

	- 🏢 Enterprise Reality (Most Used) {In real production systems you’ll mostly see:}
		- Remote Proxy (SDK / API client)
		- Protection Proxy (security)
		- Virtual Proxy (lazy load)
		- Smart Proxy (logging/resilience)
		- Caching Proxy (performance)


Reference for understanding the concept: https://refactoring.guru/design-patterns/proxy


### Decorate and Proxy looks same?
👉 Many tutorials (even some good ones) use very similar logging / authorization / caching examples for both Decorator and Proxy… which makes them look almost identical. That’s why people get confused — not because the patterns are unclear, but because the examples overlap heavily.

💡 Real Industry Truth (what actually happens):
In enterprise code: `Decorator and Proxy often LOOK the same in code.`

Sometimes even architects won’t argue whether something is Proxy or Decorator 😄 — they’ll just say:
"This is a wrapper layer."

Because modern systems mix concerns:
- logging
- auth
- caching
- retry
- telemetry

All through wrapping.

🧠 How it actually differentiate

- Don’t look at structure.
- Ask one question: 👉 "What problem was this wrapper created to solve?"
	- If answer is:
	- enhance features → Decorator
	- control access / lifecycle / location → Proxy

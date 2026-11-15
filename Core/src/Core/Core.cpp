#include "Core.h"

#include <spdlog/spdlog.h>

namespace Core
{
	Core::Core()
	{
		spdlog::set_pattern("[%T] [%^%l%$] %v");
	}

	void Core::PrintMessage()
	{
		spdlog::info("Hello from Core!");
	}
}

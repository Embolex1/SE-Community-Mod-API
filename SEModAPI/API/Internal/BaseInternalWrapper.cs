﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using VRage;

namespace SEModAPI.API.Internal
{
	public class BaseInternalWrapper
	{
		#region "Attributes"

		protected static BaseInternalWrapper m_instance;

		protected static bool m_isDebugging = false;

		#endregion

		#region "Constructors and Initializers"

		protected BaseInternalWrapper(string basePath)
		{
		}

		public static BaseInternalWrapper GetInstance(string basePath = "")
		{
			if (m_instance == null)
			{
				m_instance = new BaseInternalWrapper(basePath);
			}
			return m_instance;
		}

		#endregion

		#region "Properties"

		public static bool IsDebugging
		{
			get
			{
				GetInstance();
				return m_isDebugging;
			}
			set
			{
				GetInstance();
				m_isDebugging = value;
			}
		}

		#endregion

		#region "Methods"

		protected HashSet<Object> ConvertHashSet(Object source)
		{
			Type rawType = source.GetType();
			Type[] genericArgs = rawType.GetGenericArguments();
			MethodInfo conversion = this.GetType().GetMethod("ConvertEntityHashSet", BindingFlags.NonPublic | BindingFlags.Instance);
			conversion = conversion.MakeGenericMethod(genericArgs[0]);
			HashSet<Object> result = (HashSet<Object>)conversion.Invoke(this, new object[] { source });

			return result;
		}

		protected HashSet<Object> ConvertEntityHashSet<T>(IEnumerable<T> source)
		{
			HashSet<Object> dataSet = new HashSet<Object>();
			foreach (var rawEntity in source)
			{
				dataSet.Add(rawEntity);
			}

			return dataSet;
		}

		protected static FieldInfo GetEntityField(Object gameEntity, string fieldName)
		{
			try
			{
				FieldInfo field = gameEntity.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy);
				if(field == null)
					field = gameEntity.GetType().BaseType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy);
				return field;
			}
			catch (Exception ex)
			{
				SandboxGameAssemblyWrapper.GetMyLog().WriteLine(ex.ToString());
				return null;
			}
		}

		protected static MethodInfo GetEntityMethod(Object gameEntity, string methodName)
		{
			try
			{
				MethodInfo method = gameEntity.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy);
				return method;
			}
			catch (Exception ex)
			{
				SandboxGameAssemblyWrapper.GetMyLog().WriteLine(ex.ToString());
				return null;
			}
		}

		protected static Object InvokeEntityMethod(Object gameEntity, string methodName)
		{
			return InvokeEntityMethod(gameEntity, methodName, new object[] { });
		}

		protected static Object InvokeEntityMethod(Object gameEntity, string methodName, Object[] parameters)
		{
			try
			{
				MethodInfo method = GetEntityMethod(gameEntity, methodName);
				Object result = method.Invoke(gameEntity, parameters);

				return result;
			}
			catch (Exception ex)
			{
				SandboxGameAssemblyWrapper.GetMyLog().WriteLine(ex.ToString());
				return null;
			}
		}

		#endregion
	}
}
//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// CamaraTest.cs (28/03/2017)													\\
// Autor: Antonio Mateo (Moon Antonio) 									        \\
// Descripcion:		Permite camaras en el editor se configuren para seguir		\\
//					los objetos.												\\
// Fecha Mod:		28/03/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace Pendulum.Test
{
	/// <summary>
	/// <para>Permite camaras en el editor se configuren para seguir los objetos.</para>
	/// </summary>
	[ExecuteInEditMode]
	[AddComponentMenu("Pendulum/Test/CamaraTest")]
	public class CamaraTest : MonoBehaviour
	{
#if UNITY_EDITOR

		#region Variables Publicas
		/// <summary>
		/// <para>Activado/Desactivado</para>
		/// </summary>
		public bool on = true;										// Activado/Desactivado
		/// <summary>
		/// <para>Solo funcionar en PlayMode</para>
		/// </summary>
		public bool soloEnPlayMode = false;							// Solo funcionar en PlayMode
		/// <summary>
		/// <para>Todas las views.</para>
		/// </summary>
		public SceneViewSeguidor[] svSeguidor;						// Todas las views
		#endregion

		#region Variables Privadas
		/// <summary>
		/// <para>Todas las sceneViews del editor</para>
		/// </summary>
		private ArrayList sceneViews;                               // Todas las sceneViews del editor
		#endregion

		#region Actualizadores
		/// <summary>
		/// <para>Actualizacion.Cuando termina todo el calculo.</para>
		/// </summary>
		private void LateUpdate()// Actualizacion.Cuando termina todo el calculo
		{
			// Si hay camaras y sceneViews
			if (svSeguidor != null && sceneViews != null)
			{
				// Recorrer todas
				foreach (SceneViewSeguidor svs in svSeguidor)
				{
					// Si el target es null, fijarle uno
					if (svs.objetivo == null) svs.objetivo = transform;

					// Fijar propiedades
					svs.distancia = Mathf.Clamp(svs.distancia, .01f, float.PositiveInfinity);
					svs.sceneViewIndex = Mathf.Clamp(svs.sceneViewIndex, 0, sceneViews.Count - 1);
				}
			}

			// Si estamos en tiempo de ejecucion, activamos el seguimiento
			if (Application.isPlaying == true) Follow();
		}
		#endregion

		#region Metodos
		/// <summary>
		/// <para>Cuando se dibuja el gizmo (1xframe)</para>
		/// </summary>
		public void OnDrawGizmos()// Cuando se dibuja el gizmo (1xframe)
		{
			// Si no estamos en tiempo de ejecucion, activamos el seguimiento
			if (Application.isPlaying == false) Follow();
		}

		/// <summary>
		/// <para>Seguimiento de la vista al objetivo</para>
		/// </summary>
		private void Follow()// Seguimiento de la vista al objetivo
		{
			// Selecciona la sceneview actual
			sceneViews = UnityEditor.SceneView.sceneViews;

			// Si no se cumplen las condiciones optimas, return
			if (svSeguidor == null || !on || sceneViews.Count == 0) return;

			// Recorremos todas
			foreach (SceneViewSeguidor svs in svSeguidor)
			{
				// Si no esta activado continuamos
				if (svs.activado == false) continue;

				// Fijamos la sceneview
				UnityEditor.SceneView sceneView = (UnityEditor.SceneView)sceneViews[svs.sceneViewIndex];

				if (sceneView != null)
				{
					if ((Application.isPlaying && soloEnPlayMode) || !soloEnPlayMode)
					{
						// Seguimos
						sceneView.orthographic = svs.ortographic;
						sceneView.LookAtDirect(svs.objetivo.position + svs.posicionOffset, (svs.activarFixedRotacion) ? Quaternion.Euler(svs.fixedRotacion) : svs.objetivo.rotation, svs.distancia);
					}
				}
			}
		}
		#endregion
	}

	/// <summary>
	/// <para>Clase SceneViewSeguidor</para>
	/// </summary>
	[System.Serializable]
	public class SceneViewSeguidor
	{
		#region Variables
		/// <summary>
		/// <para>Comprueba si esta activado o si no lo esta.</para>
		/// </summary>
		public bool activado;								// Comprueba si esta activado o si no lo esta.
		/// <summary>
		/// <para>Offset de la view</para>
		/// </summary>
		public Vector3 posicionOffset;						// Offset de la view
		/// <summary>
		/// <para>Activar la fixed rotacion.</para>
		/// </summary>
		public bool activarFixedRotacion;					// Activar la fixed rotacion
		/// <summary>
		/// <para>Fixed rotacion de la view</para>
		/// </summary>
		public Vector3 fixedRotacion;						// Fixed rotacion de la view
		/// <summary>
		/// <para>Objetivo de la view</para>
		/// </summary>
		public Transform objetivo;							// Objetivo de la view
		/// <summary>
		/// <para>Distancia desde el objetivo hasta la view</para>
		/// </summary>
		public float distancia;								// Distancia desde el objetivo hasta la view
		/// <summary>
		/// <para>Activar camara ortographic</para>
		/// </summary>
		public bool ortographic;							// Activar camara ortographic
		/// <summary>
		/// <para>Sceneview indice actual.</para>
		/// </summary>
		public int sceneViewIndex;							// Sceneview indice actual
		#endregion

		#region Constructor
		/// <summary>
		/// <para>Constructor de SceneViewSeguidor</para>
		/// </summary>
		SceneViewSeguidor()
		{
			activado = false;
			posicionOffset = Vector3.zero;
			activarFixedRotacion = false;
			fixedRotacion = Vector3.zero;
			distancia = 5;
			ortographic = true;
			sceneViewIndex = 0;
		}
		#endregion
	}
#endif
}

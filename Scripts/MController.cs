//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MController.cs (28/03/2017)													\\
// Autor: Antonio Mateo (Moon Antonio) 									        \\
// Descripcion:		Controlador general del personaje							\\
// Fecha Mod:		28/03/2017													\\
// Ultima Mod:		Version inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace Pendulum.Controller
{
	/// <summary>
	/// <para>Controlador general del personaje</para>
	/// </summary>
	[AddComponentMenu("Pendulum/Controller/MController")]
	public class MController : MonoBehaviour 
	{
		#region Variables Privadas
		/// <summary>
		/// <para>Velocidad del personaje.</para>
		/// </summary>
		private float vel = 0.0f;											// Velocidad del personaje.
		/// <summary>
		/// <para>Axis horizontal.</para>
		/// </summary>
		private float axisX = 0.0f;											// Axis horizontal
		/// <summary>
		/// <para>Axis vertical.</para>
		/// </summary>
		private float axisY = 0.0f;											// Axis vertical
		/// <summary>
		/// <para>Animator del personaje.</para>
		/// </summary>
		private Animator anim;												// Animator del personaje
		/// <summary>
		/// <para>Tiempo en el que se actualizan las animaciones (MiniUpdate).</para>
		/// </summary>
		private float tempTime = 0.25f;                                     // Tiempo en el que se actualizan las animaciones
		#endregion

		#region Init
		/// <summary>
		/// <para>Iniciador de MController.</para>
		/// </summary>
		private void Start()// Iniciador de MController
		{
			anim = this.GetComponent<Animator>();

			if (anim.layerCount >= 2)
			{
				anim.SetLayerWeight(1, 1);
			}
		}
		#endregion

		#region Actualizadores
		/// <summary>
		/// <para>Actualizador de MController</para>
		/// </summary>
		private void Update()// Actualizador de MController
		{
			// Comprobamos si el animator esta
			if (anim == true)
			{
				// Obtenemos las axis
				axisX = Input.GetAxis("Horizontal");
				axisY = Input.GetAxis("Vertical");

				// Obtenemos la velocidad
				vel = new Vector2(axisX,axisY).sqrMagnitude;

				// Fijamos las animaciones
				anim.SetFloat("vel",vel);
				anim.SetFloat("dir", axisX, tempTime, Time.deltaTime);
			}
		}
		#endregion
	}
}

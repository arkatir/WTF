#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif



#include "codegen/il2cpp-codegen-metadata.h"





IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END




// 0x00000001 System.Boolean UnityEngine.AI.NavMeshAgent::SetDestination(UnityEngine.Vector3)
extern void NavMeshAgent_SetDestination_m244EFBCDB717576303DA711EE39572B865F43747 (void);
// 0x00000002 UnityEngine.Vector3 UnityEngine.AI.NavMeshAgent::get_velocity()
extern void NavMeshAgent_get_velocity_mA6F25F6B38D5092BBE6DECD77F8FDB93D5C515C9 (void);
// 0x00000003 System.Void UnityEngine.AI.NavMeshAgent::set_isStopped(System.Boolean)
extern void NavMeshAgent_set_isStopped_m3258581121A85B9F8BC02FCC2111B15506A26896 (void);
// 0x00000004 System.Single UnityEngine.AI.NavMeshAgent::get_speed()
extern void NavMeshAgent_get_speed_m5AA9A1B23412A8F5CE24A5312F6E6D4BA282B173 (void);
// 0x00000005 System.Boolean UnityEngine.AI.NavMeshAgent::SetDestination_Injected(UnityEngine.Vector3&)
extern void NavMeshAgent_SetDestination_Injected_m41607AA111EE126BBBDCDDF76B7523B0BC369D9A (void);
// 0x00000006 System.Void UnityEngine.AI.NavMeshAgent::get_velocity_Injected(UnityEngine.Vector3&)
extern void NavMeshAgent_get_velocity_Injected_m64CD1C3DAE418314D44A1194F014CEC159CDDAA8 (void);
// 0x00000007 UnityEngine.Vector3 UnityEngine.AI.NavMeshHit::get_position()
extern void NavMeshHit_get_position_m66845935ED76B2480F72EE6628EFD9D6BF35B39A_AdjustorThunk (void);
// 0x00000008 System.Void UnityEngine.AI.NavMesh::Internal_CallOnNavMeshPreUpdate()
extern void NavMesh_Internal_CallOnNavMeshPreUpdate_m5C0CEF0AEF92B6BE0368AA5ABC02B4CCDEFA9AD5 (void);
// 0x00000009 System.Boolean UnityEngine.AI.NavMesh::SamplePosition(UnityEngine.Vector3,UnityEngine.AI.NavMeshHit&,System.Single,System.Int32)
extern void NavMesh_SamplePosition_m9675E148D95E1D92ED75DC608CAA33E75ABCA05E (void);
// 0x0000000A UnityEngine.AI.NavMeshTriangulation UnityEngine.AI.NavMesh::CalculateTriangulation()
extern void NavMesh_CalculateTriangulation_mDCA19A70AA163784771574BFB905780034D41F4F (void);
// 0x0000000B System.Boolean UnityEngine.AI.NavMesh::SamplePosition_Injected(UnityEngine.Vector3&,UnityEngine.AI.NavMeshHit&,System.Single,System.Int32)
extern void NavMesh_SamplePosition_Injected_m080B714E6E15722B909226ADC8176F66A51E418A (void);
// 0x0000000C System.Void UnityEngine.AI.NavMesh::CalculateTriangulation_Injected(UnityEngine.AI.NavMeshTriangulation&)
extern void NavMesh_CalculateTriangulation_Injected_m467C53BEF1C51FE398BB4CF04A37E2E60D44D11B (void);
// 0x0000000D System.Void UnityEngine.AI.NavMesh_OnNavMeshPreUpdate::.ctor(System.Object,System.IntPtr)
extern void OnNavMeshPreUpdate__ctor_mDBB85480C3EA968112EB3B356486B9C9FF387BD4 (void);
// 0x0000000E System.Void UnityEngine.AI.NavMesh_OnNavMeshPreUpdate::Invoke()
extern void OnNavMeshPreUpdate_Invoke_m8950FEDFD3E07B272ED469FD1911AA98C60FC28D (void);
// 0x0000000F System.IAsyncResult UnityEngine.AI.NavMesh_OnNavMeshPreUpdate::BeginInvoke(System.AsyncCallback,System.Object)
extern void OnNavMeshPreUpdate_BeginInvoke_m8B7FF1B745E38190A2B744775602508E77B291FA (void);
// 0x00000010 System.Void UnityEngine.AI.NavMesh_OnNavMeshPreUpdate::EndInvoke(System.IAsyncResult)
extern void OnNavMeshPreUpdate_EndInvoke_mA263F64ADF01540E24327DDB24BD334539B1B4D2 (void);
static Il2CppMethodPointer s_methodPointers[16] = 
{
	NavMeshAgent_SetDestination_m244EFBCDB717576303DA711EE39572B865F43747,
	NavMeshAgent_get_velocity_mA6F25F6B38D5092BBE6DECD77F8FDB93D5C515C9,
	NavMeshAgent_set_isStopped_m3258581121A85B9F8BC02FCC2111B15506A26896,
	NavMeshAgent_get_speed_m5AA9A1B23412A8F5CE24A5312F6E6D4BA282B173,
	NavMeshAgent_SetDestination_Injected_m41607AA111EE126BBBDCDDF76B7523B0BC369D9A,
	NavMeshAgent_get_velocity_Injected_m64CD1C3DAE418314D44A1194F014CEC159CDDAA8,
	NavMeshHit_get_position_m66845935ED76B2480F72EE6628EFD9D6BF35B39A_AdjustorThunk,
	NavMesh_Internal_CallOnNavMeshPreUpdate_m5C0CEF0AEF92B6BE0368AA5ABC02B4CCDEFA9AD5,
	NavMesh_SamplePosition_m9675E148D95E1D92ED75DC608CAA33E75ABCA05E,
	NavMesh_CalculateTriangulation_mDCA19A70AA163784771574BFB905780034D41F4F,
	NavMesh_SamplePosition_Injected_m080B714E6E15722B909226ADC8176F66A51E418A,
	NavMesh_CalculateTriangulation_Injected_m467C53BEF1C51FE398BB4CF04A37E2E60D44D11B,
	OnNavMeshPreUpdate__ctor_mDBB85480C3EA968112EB3B356486B9C9FF387BD4,
	OnNavMeshPreUpdate_Invoke_m8950FEDFD3E07B272ED469FD1911AA98C60FC28D,
	OnNavMeshPreUpdate_BeginInvoke_m8B7FF1B745E38190A2B744775602508E77B291FA,
	OnNavMeshPreUpdate_EndInvoke_mA263F64ADF01540E24327DDB24BD334539B1B4D2,
};
static const int32_t s_InvokerIndices[16] = 
{
	1504,
	1499,
	31,
	728,
	839,
	6,
	1499,
	3,
	2370,
	2371,
	2372,
	17,
	124,
	23,
	105,
	26,
};
extern const Il2CppCodeGenModule g_UnityEngine_AIModuleCodeGenModule;
const Il2CppCodeGenModule g_UnityEngine_AIModuleCodeGenModule = 
{
	"UnityEngine.AIModule.dll",
	16,
	s_methodPointers,
	s_InvokerIndices,
	0,
	NULL,
	0,
	NULL,
	0,
	NULL,
	NULL,
};

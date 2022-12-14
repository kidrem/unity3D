using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
	public GameObject disk_prefab = null;                 
	private List<DiskData> used = new List<DiskData>();   
	private List<DiskData> free = new List<DiskData>();   

	public GameObject GetDisk(int round)
	{        
		float start_y = -10f;                             
		string tag;
		disk_prefab = null;
		if (round == 1)
		{
			tag = "disk1";;
		}
		else if(round == 2)
		{
			tag = "disk2";
		}
		else
		{
			tag = "disk3";
		}
		for(int i=0;i<free.Count;i++)
		{
			if(free[i].tag == tag)
			{
				disk_prefab = free[i].gameObject;
				free.Remove(free[i]);
				break;
			}
		}
		if(disk_prefab == null)
		{
			if (tag == "disk1")
			{
				disk_prefab = Instantiate(Resources.Load<GameObject>("Prefabs/disk1"), new Vector3(0, start_y, 0), Quaternion.identity);

			}
			else if (tag == "disk2")
			{
				disk_prefab = Instantiate(Resources.Load<GameObject>("Prefabs/disk2"), new Vector3(0, start_y, 0), Quaternion.identity);
				disk_prefab.GetComponent<DiskData> ().score = 2;
			}
			else
			{
				disk_prefab = Instantiate(Resources.Load<GameObject>("Prefabs/disk3"), new Vector3(0, start_y, 0), Quaternion.identity);
				disk_prefab.GetComponent<DiskData> ().score = 3;
			}
			float ran_x = Random.Range(-1f, 1f) < 0 ? -1 : 1;
			disk_prefab.GetComponent<MeshRenderer> ().material.color = disk_prefab.GetComponent<DiskData>().color;
			disk_prefab.GetComponent<DiskData>().direction = new Vector3(ran_x, start_y, 0);
			disk_prefab.GetComponent<DiskData> ().tag = tag;
			disk_prefab.transform.localScale = disk_prefab.GetComponent<DiskData>().scale;
		}
		used.Add(disk_prefab.GetComponent<DiskData>());
		return disk_prefab;
	}
	public void FreeDisk(GameObject disk)
	{
		for(int i = 0;i < used.Count; i++)
		{
			if (disk.GetInstanceID() == used[i].gameObject.GetInstanceID())
			{
				used[i].gameObject.SetActive(false);
				free.Add(used[i]);
				used.Remove(used[i]);
				break;
			}
		}
	}
}
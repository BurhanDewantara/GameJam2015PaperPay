using UnityEngine;
using UnityEditor;
using System.Collections;


class ProjectAssetPostprocessor : AssetPostprocessor {
	void OnPreprocessAudio() {
		if (assetPath.EndsWith(".wav") || assetPath.EndsWith(".mp3")) {
		preprocessMusic();
		}
	}

	void preprocessMusic() {
		AudioImporter importer = (AudioImporter)assetImporter;

		importer.format = AudioImporterFormat.Compressed;
		importer.threeD = false;
		importer.forceToMono = true;
		importer.loadType = AudioImporterLoadType.DecompressOnLoad;
		importer.hardware = false;
		importer.loopable = true;
		importer.compressionBitrate = 128000;
	}
}
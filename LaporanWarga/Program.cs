using System;
using System.Data;
using System.Data.SqlClient;

namespace LaporanWarga
{
    class Program
    {
        static SqlConnection conn;

        static void ConnectDB()
        {
            try
            {
                string strKoneksi = "Data Source=LAPTOP-ALVIEN\\ALVIEN_RIDHO;" +
                                     "Initial Catalog=LaporanWarga;Integrated Security=True;";
                conn = new SqlConnection(strKoneksi);
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error koneksi database: {ex.Message}");
            }
        }

        static void DisconnectDB()
        {
            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error menutup koneksi database: {ex.Message}");
            }
        }

        static void InputWarga(string nik, string nama, string alamat, string no_telp)
        {
            if (string.IsNullOrWhiteSpace(nik) || string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(alamat) || string.IsNullOrWhiteSpace(no_telp))
            {
                Console.WriteLine("Semua data harus diisi. Input data warga gagal.");
                return;
            }
            if (!IsNumeric(nik))
            {
                Console.WriteLine("NIK hanya boleh berisi angka. Input data warga gagal.");
                return;
            }
            if (!IsNumeric(no_telp))
            {
                Console.WriteLine("Nomor telepon hanya boleh berisi angka. Input data warga gagal.");
                return;
            }
            try
            {
                string query = $"INSERT INTO Wargaa (NIK, Nama, Alamat, No_Telp) VALUES ('{nik}', '{nama}', '{alamat}', '{no_telp}')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Data warga berhasil disimpan.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error input data warga: {ex.Message}");
            }
        }

        static void UpdateWarga(string nik, string nama, string alamat, string no_telp)
        {
            if (string.IsNullOrWhiteSpace(nik) || string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(alamat) || string.IsNullOrWhiteSpace(no_telp))
            {
                Console.WriteLine("Semua data harus diisi. Input data warga gagal.");
                return;
            }
            if (!IsNumeric(nik))
            {
                Console.WriteLine("NIK hanya boleh berisi angka. Update data warga gagal.");
                return;
            }
            if (!IsNumeric(no_telp))
            {
                Console.WriteLine("Nomor telepon hanya boleh berisi angka. Update data warga gagal.");
                return;
            }

            try
            {
                string query = $"UPDATE Wargaa SET Nama='{nama}', Alamat='{alamat}', No_Telp='{no_telp}' WHERE NIK='{nik}'";
                SqlCommand cmd = new SqlCommand(query, conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data warga berhasil diupdate.");
                }
                else
                {
                    Console.WriteLine("NIK warga tidak ditemukan.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error update data warga: {ex.Message}");
            }
        }

        static bool IsNumeric(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        static void HapusWarga(string nik)
        {
            try
            {
                if (!DataWargaExists(nik))
                {
                    Console.WriteLine("NIK warga tidak ditemukan.");
                    return;
                }

                Console.WriteLine($"Apakah Anda yakin ingin menghapus data warga dengan NIK {nik}? (Y/N)");
                string confirmation = Console.ReadLine();

                if (confirmation.ToUpper() == "Y")
                {
                    string query = $"DELETE FROM Wargaa WHERE NIK='{nik}'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data warga berhasil dihapus.");
                    }
                    else
                    {
                        Console.WriteLine("Terjadi kesalahan saat menghapus data warga.");
                    }
                }
                else
                {
                    Console.WriteLine("Penghapusan data warga dibatalkan.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error hapus data warga: {ex.Message}");
            }
        }

        static bool DataWargaExists(string nik)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM Wargaa WHERE NIK='{nik}'";
                SqlCommand cmd = new SqlCommand(query, conn);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error memeriksa keberadaan data warga: {ex.Message}");
                return false;
            }
        }

        static void TampilkanDataWarga()
        {
            try
            {
                string query = "SELECT * FROM Wargaa";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("\nData Wargaa:");
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine($"{reader.GetName(i)}: {reader.GetValue(i)}");
                        }
                        Console.WriteLine("------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("Tidak ada data warga yang tersedia.");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error menampilkan data warga: {ex.Message}");
            }
        }

        static void InputPerangkatRT(string nip, string nama_petugas, string jabatan_petugas)
        {
            if (string.IsNullOrWhiteSpace(nip) || string.IsNullOrWhiteSpace(nama_petugas) || string.IsNullOrWhiteSpace(jabatan_petugas))
            {
                Console.WriteLine("Semua data harus diisi. Input data perangkat RT gagal.");
                return;
            }
            if (!IsNumeric(nip))
            {
                Console.WriteLine("NIK hanya boleh berisi angka. Input data perangkat RT gagal.");
                return;
            }
            try
            {
                string query = $"INSERT INTO PerangkatRT (NIP, Nama_Petugas, Jabatan_Petugas) VALUES ('{nip}', '{nama_petugas}', '{jabatan_petugas}')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Data perangkat RT berhasil disimpan.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error input data perangkat RT: {ex.Message}");
            }
        }


        static void InputLaporan(string id_pengaduan, string jns_pengaduan, string desk_pengaduan, string nip, string nik)
        {
            if (string.IsNullOrWhiteSpace(id_pengaduan) || string.IsNullOrWhiteSpace(jns_pengaduan) || string.IsNullOrWhiteSpace(desk_pengaduan) || string.IsNullOrWhiteSpace(nik) || string.IsNullOrWhiteSpace(nip))
            {
                Console.WriteLine("Semua data harus diisi. Input data warga gagal.");
                return;
            }
            if (!IsNumeric(id_pengaduan))
            {
                Console.WriteLine("id_pengaduan hanya boleh berisi angka. Input laporan gagal.");
                return;
            }
            if (!IsNumeric(nik))
            {
                Console.WriteLine("NIK hanya boleh berisi angka. Input laporan gagal.");
                return;
            }
            if (!IsNumeric(nip))
            {
                Console.WriteLine("NIP hanya boleh berisi angka. Input laporan gagal.");
                return;
            }
            try
            {
                string sts_pengaduan = "PROSES";
                string query = $"INSERT INTO Pengaduan (ID_Pengaduan, Jns_Pengaduan, Desk_Pengaduan, NIP, NIK, Sts_Pengaduan) VALUES ('{id_pengaduan}', '{jns_pengaduan}', '{desk_pengaduan}', '{nip}', '{nik}', '{sts_pengaduan}')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Data laporan berhasil disimpan.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error input data laporan: {ex.Message}");
            }
        }

        static void UpdateLaporan(string id_pengaduan, string sts_pengaduan)
        {
            if (string.IsNullOrWhiteSpace(id_pengaduan) || string.IsNullOrWhiteSpace(sts_pengaduan))
            {
                Console.WriteLine("Semua data harus diisi. Input data warga gagal.");
                return;
            }
            if (!IsNumeric(id_pengaduan))
            {
                Console.WriteLine("id_pengaduan hanya boleh berisi angka. Update laporan gagal.");
                return;
            }
            try
            {
                if (sts_pengaduan == "SELESAI" || sts_pengaduan == "PROSES" || sts_pengaduan == "DITOLAK")
                {
                    string query = $"UPDATE Pengaduan SET Sts_Pengaduan='{sts_pengaduan}' WHERE ID_Pengaduan='{id_pengaduan}'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Status laporan berhasil diupdate.");
                    }
                    else
                    {
                        Console.WriteLine("ID Pengaduan tidak ditemukan.");
                    }
                }
                else
                {
                    Console.WriteLine("Status pengaduan tidak valid. Silakan masukkan 'SELESAI', 'PROSES', atau 'DITOLAK'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error update status laporan: {ex.Message}");
            }
        }

        static void HapusLaporan(string id_pengaduan)
        {
            try
            {
                if (!LaporanExists(id_pengaduan))
                {
                    Console.WriteLine("ID Pengaduan tidak ditemukan.");
                    return;
                }

                Console.WriteLine($"Apakah Anda yakin ingin menghapus laporan dengan ID {id_pengaduan}? (Y/N)");
                string confirmation = Console.ReadLine();

                if (confirmation.ToUpper() == "Y")
                {
                    string query = $"DELETE FROM Pengaduan WHERE ID_Pengaduan='{id_pengaduan}'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data laporan berhasil dihapus.");
                    }
                    else
                    {
                        Console.WriteLine("Terjadi kesalahan saat menghapus data laporan.");
                    }
                }
                else
                {
                    Console.WriteLine("Penghapusan data laporan dibatalkan.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error hapus data laporan: {ex.Message}");
            }
        }

        static bool LaporanExists(string id_pengaduan)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM Pengaduan WHERE ID_Pengaduan='{id_pengaduan}'";
                SqlCommand cmd = new SqlCommand(query, conn);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error memeriksa keberadaan data laporan: {ex.Message}");
                return false;
            }
        }

        static void TampilkanDataLaporan(string keyField, string keyValue)
        {
            try
            {
                string query;
                SqlCommand cmd;

                if (string.IsNullOrEmpty(keyValue))
                {
                    query = "SELECT * FROM Pengaduan";
                    cmd = new SqlCommand(query, conn);
                }
                else
                {
                    query = $"SELECT * FROM Pengaduan WHERE {keyField} = '{keyValue}'";
                    cmd = new SqlCommand(query, conn);
                }

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("\nData Laporan:");
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine($"{reader.GetName(i)}: {reader.GetValue(i)}");
                        }
                        Console.WriteLine("------------------------");
                    }
                }
                else
                {
                    Console.WriteLine($"Data Laporan dengan {keyField} {keyValue} tidak ditemukan.");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error menampilkan data Laporan: {ex.Message}");
            }
        }

        static void TampilkanDataBaru(string tableName, string keyField, string keyValue)
        {
            try
            {
                string query = $"SELECT * FROM {tableName} WHERE {keyField} = '{keyValue}'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine($"\nData baru dari {tableName}:");
                    while (reader.Read())
                    {
                        foreach (DataRow column in reader.GetSchemaTable().Rows)
                        {
                            string columnName = column["ColumnName"].ToString();
                            int columnIndex = reader.GetOrdinal(columnName);
                            Console.WriteLine($"{columnName}: {reader.GetValue(columnIndex)}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Data {tableName} dengan {keyField} {keyValue} tidak ditemukan.");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error menampilkan data {tableName}: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            ConnectDB();

            int pilihan;
            int subPilihan;
            do
            {
                Console.WriteLine("\nAplikasi Laporan Warga");
                Console.WriteLine("1. Input Data Warga");
                Console.WriteLine("2. Mengelola Data Warga");
                Console.WriteLine("3. Input Data Perangkat RT");
                Console.WriteLine("4. Input Laporan");
                Console.WriteLine("5. Mengelola Laporan");
                Console.WriteLine("6. Keluar");
                Console.Write("Pilih menu: ");
                if (!int.TryParse(Console.ReadLine(), out pilihan))
                {
                    Console.WriteLine("Masukkan angka menu yang valid.");
                    continue;
                }

                switch (pilihan)
                {
                    case 1:
                        Console.Write("NIK: ");
                        string nik = Console.ReadLine();
                        Console.Write("Nama: ");
                        string namaWarga = Console.ReadLine();
                        Console.Write("Alamat: ");
                        string alamat = Console.ReadLine();
                        Console.Write("No. Telepon: ");
                        string noTelp = Console.ReadLine();
                        InputWarga(nik, namaWarga, alamat, noTelp);
                        TampilkanDataBaru("Wargaa", "NIK", nik);
                        break;

                    case 2:
                        do
                        {
                            Console.WriteLine("\nMenu Mengelola Data Warga");
                            Console.WriteLine("1. Update Data Warga");
                            Console.WriteLine("2. Hapus Data Warga");
                            Console.WriteLine("3. Tampilkan Data Warga");
                            Console.WriteLine("4. Keluar");
                            Console.Write("Pilih sub-menu: ");
                            if (!int.TryParse(Console.ReadLine(), out subPilihan))
                            {
                                Console.WriteLine("Masukkan angka sub-menu yang valid.");
                                continue;
                            }

                            switch (subPilihan)
                            {
                                case 1:
                                    Console.Write("NIK: ");
                                    string nikUpdate = Console.ReadLine();
                                    Console.Write("Nama Baru: ");
                                    string namaBaru = Console.ReadLine();
                                    Console.Write("Alamat Baru: ");
                                    string alamatBaru = Console.ReadLine();
                                    Console.Write("No. Telepon Baru: ");
                                    string noTelpBaru = Console.ReadLine();
                                    UpdateWarga(nikUpdate, namaBaru, alamatBaru, noTelpBaru);
                                    break;
                                case 2:
                                    Console.Write("NIK: ");
                                    string nikHapus = Console.ReadLine();
                                    HapusWarga(nikHapus);
                                    break;
                                case 3:
                                    TampilkanDataWarga(); 
                                    break;
                                case 4:
                                    break;
                                default:
                                    Console.WriteLine("Sub-menu tidak valid.");
                                    break;
                            }
                        } while (subPilihan != 4);
                        break;

                    case 3:
                        Console.Write("NIP: ");
                        string nip = Console.ReadLine();
                        Console.Write("Nama: ");
                        string namaPetugas = Console.ReadLine();
                        Console.Write("Jabatan: ");
                        string jabatan = Console.ReadLine();
                        InputPerangkatRT(nip, namaPetugas, jabatan);
                        TampilkanDataBaru("PerangkatRT", "NIP", nip);
                        break;

                    case 4:
                        Console.Write("ID Pengaduan: ");
                        string idPengaduan = Console.ReadLine();
                        Console.Write("Jenis Pengaduan: ");
                        string jnsPengaduan = Console.ReadLine();
                        Console.Write("Deskripsi Pengaduan: ");
                        string deskPengaduan = Console.ReadLine();
                        Console.Write("NIP: ");
                        string nipLaporan = Console.ReadLine();
                        Console.Write("NIK: ");
                        string nikLaporan = Console.ReadLine();
                        InputLaporan(idPengaduan, jnsPengaduan, deskPengaduan, nipLaporan, nikLaporan);
                        TampilkanDataBaru("Pengaduan", "ID_Pengaduan", idPengaduan);
                        break;

                    case 5:
                        do
                        {
                            Console.WriteLine("\nMenu Mengelola Laporan");
                            Console.WriteLine("1. Update Status Laporan");
                            Console.WriteLine("2. Hapus Laporan");
                            Console.WriteLine("3. Tampilkan Data Laporan");
                            Console.WriteLine("4. Keluar");
                            Console.Write("Pilih sub-menu: ");
                            if (!int.TryParse(Console.ReadLine(), out subPilihan))
                            {
                                Console.WriteLine("Masukkan angka sub-menu yang valid.");
                                continue;
                            }

                            switch (subPilihan)
                            {
                                case 1:
                                    Console.Write("ID Pengaduan: ");
                                    string idPengaduanUpdate = Console.ReadLine();
                                    Console.Write("Status Baru ('SELESAI', 'PROSES', atau 'DITOLAK'): ");
                                    string stsPengaduanUpdate = Console.ReadLine().ToUpper(); 
                                    UpdateLaporan(idPengaduanUpdate, stsPengaduanUpdate);
                                    break;
                                case 2:
                                    Console.Write("ID Pengaduan: ");
                                    string idPengaduanHapus = Console.ReadLine();
                                    HapusLaporan(idPengaduanHapus);
                                    break;
                                case 3:
                                    Console.Write("ID Pengaduan: ");
                                    string idPengaduanTampilkan = Console.ReadLine();
                                    TampilkanDataLaporan("ID_Pengaduan", idPengaduanTampilkan);
                                    break;
                                case 4:
                                    break; 
                                default:
                                    Console.WriteLine("Sub-menu tidak valid.");
                                    break;
                            }
                        } while (subPilihan != 4);
                        break;

                    case 6:
                        Console.WriteLine("Terima kasih sudah menggunakan aplikasi ini.");
                        break;

                    default:
                        Console.WriteLine("Menu tidak valid.");
                        break;
                }
            } while (pilihan != 6);

            DisconnectDB();
        }
    }
}
function BinaryReader(dt) {
    this.dt = dt;
    this.dt8 = new Uint8Array(dt);
    this.Position = 0;
    this.Length = dt.byteLength;
}

BinaryReader.prototype.ReadInt = function () {
    if (this.Position+4>this.Length) {
        //todo
        return 0;
    }
    var r = this.dt8[this.Position] + (this.dt8[this.Position + 1] << 8)
        + (this.dt8[this.Position + 2] << 16) + (this.dt8[this.Position + 3] << 24)
    this.Position += 4;
    return r;
};

BinaryReader.prototype.ReadString = function(){
    var len = 0; var base = 1; 
    var p = this.Position + 4;
    for (var i = this.Position; i < p; i++) {
       
        if ((this. dt8[i] & 128) == 1) {
            len += (this.dt8[i] & 127) * base;
            base = base * 128;
            this.Position++;
        } else {
            len += (this.dt8[i] & 127) * base;
            this.Position++;
            break;
        }
    }
    if (this.Position + len > this.Length) {
        //todo
        return "";
    }
    var part = new Uint8Array(this.dt, this.Position, len);
    var str = new TextDecoder().decode(part);
    this.Position += len;
    return str;
}

BinaryReader.prototype.ReadByte = function () {
    if (this.Position + 1 > this.Length) {
        //todo
        return 0;
    }
    var r = this.dt8[this.Position];
    this.Position += 1;
    return r;
};

BinaryReader.prototype.ReadBytes = function (length) {
    if (this.Position + length > this.Length) {
        //todo
        return 0;
    }
    var r = new Uint8Array(this.dt, this.Position, length);
    this.Position += length;
    return r ;
};

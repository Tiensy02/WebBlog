import BaseService from "./base-service";
class RattingService extends BaseService{
    constructor(){
        super("https://localhost:7070/api/Ratting")
    }
}
export default RattingService
<template>
    <fieldset ref="ratingCore" class="rating">
        <input @change="changeRating" class="inputRating" ref="rating5" type="radio" id="star5" name="rating" value="5" />
        <label ref="labelRating5" :class="{ 'is-rating': !isRating }" for="star5" class="full" title="Awesome"></label>

        <input @change="changeRating" class="inputRating" ref="rating45" type="radio" id="star4.5" name="rating"
            value="4.5" />
        <label ref="labelRating45" :class="{ 'is-rating': !isRating }" for="star4.5" class="half"></label>

        <input @change="changeRating" class="inputRating" ref="rating4" type="radio" id="star4" name="rating" value="4" />
        <label ref="labelRating4" :class="{ 'is-rating': !isRating }" for="star4" class="full"></label>

        <input @change="changeRating" class="inputRating" ref="rating35" type="radio" id="star3.5" name="rating"
            value="3.5" />
        <label ref="labelRating35" :class="{ 'is-rating': !isRating }" for="star3.5" class="half"></label>

        <input @change="changeRating" class="inputRating" ref="rating3" type="radio" id="star3" name="rating" value="3" />
        <label ref="labelRating3" :class="{ 'is-rating': !isRating }" for="star3" class="full"></label>

        <input @change="changeRating" class="inputRating" ref="rating25" type="radio" id="star2.5" name="rating"
            value="2.5" />
        <label ref="labelRating25" :class="{ 'is-rating': !isRating }" for="star2.5" class="half"></label>

        <input @change="changeRating" class="inputRating" ref="rating2" type="radio" id="star2" name="rating" value="2" />
        <label ref="labelRating2" :class="{ 'is-rating': !isRating }" for="star2" class="full"></label>

        <input @change="changeRating" class="inputRating" ref="rating15" type="radio" id="star1.5" name="rating"
            value="1.5" />
        <label ref="labelRating15" :class="{ 'is-rating': !isRating }" for="star1.5" class="half"></label>

        <input @change="changeRating" class="inputRating" ref="rating1" type="radio" id="star1" name="rating" value="1" />
        <label ref="labelRating1" :class="{ 'is-rating': !isRating }" for="star1" class="full"></label>

        <input @change="changeRating" class="inputRating" ref="rating05" type="radio" id="star0.5" name="rating"
            value="0.5" />
        <label ref="labelRating05" :class="{ 'is-rating': !isRating }" for="star0.5" class="half"></label>
    </fieldset>
</template>
<script>
import { ref } from 'vue'
import ToastMess from '../../components/ToastMess/ToastMess.vue';
import createToast from '../../helpper/createToastMess.js';
import PostService from '../../service/post-service.js';
export default {
    name: "rating-star",
    setup() {
        const ratings = [];
        const labelRatings = []
        const ratingCore = ref(null)
        const rating5 = ref(null)
        const rating45 = ref(null)
        const rating4 = ref(null)
        const rating35 = ref(null)
        const rating3 = ref(null)
        const rating25 = ref(null)
        const rating2 = ref(null)
        const rating15 = ref(null)
        const rating1 = ref(null)
        const rating05 = ref(null)
        const labelRating5 = ref(null);
        const labelRating45 = ref(null);
        const labelRating4 = ref(null);
        const labelRating35 = ref(null);
        const labelRating3 = ref(null);
        const labelRating25 = ref(null);
        const labelRating2 = ref(null);
        const labelRating15 = ref(null);
        const labelRating1 = ref(null);
        const labelRating05 = ref(null);
        ratings.push(rating5, rating45, rating4, rating35, rating3, rating25, rating2, rating15, rating1, rating05)
        labelRatings.unshift(
            labelRating05,
            labelRating1,
            labelRating15,
            labelRating2,
            labelRating25,
            labelRating3,
            labelRating35,
            labelRating4,
            labelRating45,
            labelRating5)
        return {
            labelRating45,
            labelRating4,
            labelRating35,
            labelRating3,
            labelRating25,
            labelRating2,
            labelRating15,
            labelRating1,
            labelRating05,
            labelRatings,
            labelRating5,
            rating05, rating1, rating15, rating2, rating25, rating3, ratingCore, rating5, rating4, rating45, rating35, ratings
        }
    },
    mounted() {
        if(!this.user) {
            this.isRating = false
        }
        this.selectValue(this.ratingCoreValue)
    },
    watch: {
        ratingCoreValue(newValue) {
            this.selectValue(newValue)
        }
    },
    data() {
        return {
            numberRating: 0,
            isRating: this.PropIsRating,
            user : JSON.parse(localStorage.getItem("user")),
        }
    },
    methods: {
        /**
         * @description hàm thực hiện chuyển điểm về dạng mong muốn 
         * @param {double} core điểm nhận được prop
         */
        converRatingCore(core) {
            core = Number(core)
            var newCore = Number(core.toFixed(2))
            if (newCore % 1 == 0) {
                return newCore
            } else {

                var coreDecimal = (newCore % 1).toFixed(2);
                if (coreDecimal <= 0.25) {
                    return newCore - coreDecimal
                } else if (coreDecimal <= 0.5) {
                    return newCore + (0.5 - coreDecimal)
                } else {
                    return newCore.toFixed(0)
                }
            }
        },
        /**
         * @description hàm thực hiện chọn sao dựa trên giá trị nhận được
         * @param {double} value điểm
         */
        selectValue(value) {
            var count = 0
            var coreConver = this.converRatingCore(value)
            this.ratings.forEach(elem => {
                if (elem._value.value == coreConver) {
                    elem._value.checked = true
                    this.addClassSelectedForLabel(coreConver)
                }
            })
        },
        addClassSelectedForLabel(core) {
            for (let i = 0; i < core * 2; i++) {
                this.labelRatings[i]._value.classList.add("rating-selected")
            }
        },
        /**
         * @description hàm thực hiện thay đổi lượt đánh giá, điểm đánh giá và huỷ trạng thái đánh giá sau khi người dùng đánh giá thành công
         * @param {*} e biến event
         */
        changeRating(e) {
            this.numberRating++
            if (this.numberRating == 1) {
                this.labelRatings.forEach(elem => {
                    elem._value.classList.add("is-rating")
                })
                let valueCore = this.numberRatingOfPost>0? e.target.value : e.target.value * 2
                new PostService().updatePostRating(valueCore,this.postID)
                .then(res => {
                    createToast(this.$t("userAction.ratingInserSuccess"))
                    this.$emit("ratingSuccess")
                })
                .catch(err => {
                    console.log(err)
                })
            }
        }
    },
    props: {
        //điểm đánh giá ban đầu của bài viết
        ratingCoreValue: {
            type: Number,
            default: 0
        },
        // có cho phép đánh giá hay không
        PropIsRating: {
            type: Boolean,
            default: true
        },
        // id của bài viết
        postID: {

        },
        numberRatingOfPost:{
            type:Number,

        }

    },
    components: {
        ToastMess
    }
}
</script>
<style @scoped>
@import url('./rating.css');
</style>